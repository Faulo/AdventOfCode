node('dotnet') {
	checkout scm
	
	def solutions = [
		'Year2015', 'Year2016', 'Year2017', 'Year2018', 'Year2019', 'Year2020', 'Year2021', 'Year2022', 'Year2023', 'Year2024'
	]
	
	def projects = [
		'Day01', 'Day02', 'Day03', 'Day04', 'Day05', 'Day06', 'Day07', 'Day08', 'Day09', 'Day10',
		'Day11', 'Day12', 'Day13', 'Day14', 'Day15', 'Day16', 'Day17', 'Day18', 'Day19', 'Day20',
		'Day21', 'Day22', 'Day23', 'Day24', 'Day25'
	]
	
	for (solution in solutions) {
		for (project in projects) {
			def path = "${solution}/${project}"
			if (fileExists(path)) {
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
		}
	}
}