pipeline {
	agent any

	stages {
		stage('Building') {
			steps {
				dir('AdventOfCode.Year2022') {
					callShell 'dotnet build'
				}
			}
		}
		stage('Testing') {
			steps {
				dir('AdventOfCode.Year2022') {
					sh(script: 'dotnet test --logger junit', returnStatus: true)
					junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
				}
			}
		}
		stage('Running: Year 2022, Day 01') {
			steps {
				dir('AdventOfCode.Year2022/Day01') {
					callShell 'dotnet run'
				}
			}
		}
		stage('Running: Year 2022, Day 02') {
			steps {
				dir('AdventOfCode.Year2022/Day02') {
					callShell 'dotnet run'
				}
			}
		}
		stage('Running: Year 2022, Day 03') {
			steps {
				dir('AdventOfCode.Year2022/Day03') {
					callShell 'dotnet run'
				}
			}
		}
		stage('Running: Year 2022, Day 04') {
			steps {
				dir('AdventOfCode.Year2022/Day04') {
					callShell 'dotnet run'
				}
			}
		}
		stage('Running: Year 2022, Day 05') {
			steps {
				dir('AdventOfCode.Year2022/Day05') {
					callShell 'dotnet run'
				}
			}
		}
		stage('Running: Year 2022, Day 06') {
			steps {
				dir('AdventOfCode.Year2022/Day06') {
					callShell 'dotnet run'
				}
			}
		}
		stage('Running: Year 2022, Day 07') {
			steps {
				dir('AdventOfCode.Year2022/Day07') {
					callShell 'dotnet run'
				}
			}
		}
	}
}