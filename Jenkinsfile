def prepare(branches, solution, version) {	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	for (project in projects) {
		def path = "${solution}/${project}"
		if (fileExists(path)) {
			branches[path] = {
				build(path, version)
			}
		}
	}
}

def build(path, version) {
    dir(path) {
        withDockerContainer(image: "mcr.microsoft.com/devcontainers/dotnet:${version}") {
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
	
	prepare(branches, 'Year2022', '7.0')
	
	prepare(branches, 'Year2023', '8.0')
	
	parallel branches
}