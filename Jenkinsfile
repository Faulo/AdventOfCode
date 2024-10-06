def prepare(solution, version) {	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	return {
		if (fileExists(solution)) {
			dir(solution) {
				def unix = isUnix()
				def tag = unix
					? version
					: version + "-nanoserver-1809" // "-windowsservercore-ltsc2019"
				withDockerContainer(image: "mcr.microsoft.com/dotnet/sdk:${tag}") {
					stage("${solution}: restore") {
						if (unix) {
							sh 'dotnet restore'
						} else {
							bat 'dotnet restore'
						}
					}
					
					for (project in projects) {
						def path = "${solution}/${project}"
						if (fileExists(project)) {
							dir(project) {
								build(path, unix)
							}
						}
					}
				}
			}
		}
	}
}

def build(path, unix) {
	stage("${path}: build") {
		if (unix) {
			sh 'dotnet build'
		} else {
			bat 'dotnet build'
		}
	}
	stage("${path}: test") {
		if (unix) {
			sh 'dotnet test --logger junit'
		} else {
			bat 'dotnet test --logger junit'
		}
		junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
	}
	stage("${path}: run") {
		if (unix) {
			sh 'dotnet run'
		} else {
			bat 'dotnet run'
		}
	}
}

properties([
	disableConcurrentBuilds(),
	disableResume()
])

node('docker') {
	checkout scm
	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	def branches = [:]
	
	branches['Year2022'] = prepare('Year2022', '7.0') //-nanoserver-1809
	branches['Year2023'] = prepare('Year2023', '8.0') //-nanoserver-1809
	
	parallel branches
}