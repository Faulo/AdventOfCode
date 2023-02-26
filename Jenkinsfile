pipeline {
	stages {
		stage('Build') {
			steps {
				dir('AdventOfCode.Year2023') {
					callShell 'dotnet build'
				}
			}
		}
	}
}