pipeline {
	agent any

	stages {
		stage('Building') {
			steps {
				dir('AdventOfCode.Year2023') {
					callShell 'dotnet build'
				}
			}
		}
		stage('Testing') {
			steps {
				dir('AdventOfCode.Year2023') {
					callShell 'dotnet test --logger junit'
					junit(testResults: '**/report.xml', allowEmptyResults: true)
				}
			}
		}
	}
}