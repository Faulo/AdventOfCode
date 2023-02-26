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
					callShell 'dotnet test --logger "trx;LogFileName=report.xml"'
					step([$class: 'MSTestPublisher', testResultsFile:"**/report.xml", failOnError: true, keepLongStdio: true])
				}
			}
		}
		
	}
}