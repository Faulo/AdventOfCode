def prepare(solution, version) {	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day24', 'Day25'
	]
	
	return {
		if (fileExists(solution)) {
			dir(solution) {
				withDockerContainer(image: "mcr.microsoft.com/dotnet/sdk:${version}") {
					stage("${solution}: restore") {
						callShell 'dotnet restore'
					}
					
					for (project in projects) {
						def path = "${solution}/${project}"
						if (fileExists(project)) {
							dir(project) {
								build(path, version)
							}
						}
					}
				}
			}
		}
	}
}

def build(path, version) {
	stage("${path}: build") {
		callShell 'dotnet build'
	}
	stage("${path}: test") {
		callShell 'dotnet test --logger junit'
		junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
	}
	stage("${path}: run") {
		callShell 'dotnet run'
	}
}

properties([
	disableConcurrentBuilds(),
	disableResume()
])

node('linux && docker') {
	checkout scm
	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	def branches = [:]
	
	branches['Year2022'] = prepare('Year2022', '7.0')
	branches['Year2023'] = prepare('Year2023', '8.0')
	
	parallel branches
}