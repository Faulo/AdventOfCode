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
					callShell 'dotnet test --logger junit'
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
	}
}