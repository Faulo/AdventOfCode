def prepare(solution, version) {	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	return {
        withDockerContainer(image: "mcr.microsoft.com/dotnet/sdk:${version}") {
			for (project in projects) {
				def path = "${solution}/${project}"
				if (fileExists(path)) {
					build(path, version)
				}
			}
		}
	}
}

def build(path, version) {
    dir(path) {
		stage("${path}: Build") {
			sh 'dotnet build'
		}
		stage("${path}: Test") {
			sh(script: 'dotnet test --logger junit', returnStatus: true)
			junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
		}
		stage("${path}: Run") {
			sh 'dotnet run'
		}
    }
}

properties([
	disableConcurrentBuilds(),
	disableResume()
])

node('windows && docker') {
	checkout scm
	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	def branches = [:]
	
	branches['Year2022'] = prepare('Year2022', '7.0-nanoserver-1809')
	branches['Year2023'] = prepare('Year2023', '8.0-nanoserver-1809')
	
	parallel branches
}