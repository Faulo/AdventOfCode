pipeline {
	agent {
		label 'docker'
	}
	environment {
		DOCKER_IMAGE_LINUX = "mcr.microsoft.com/dotnet/sdk:8.0"
		DOCKER_IMAGE_WINDOWS = "mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2019"
		DOCKER_ARGS = " "
	}
	options {
		disableConcurrentBuilds()
		disableResume()
	}
	stages {
		stage('Index workspace') {
			steps {
				script {
					def useMultithreading = false

					def years = [
						'2015',
						'2016',
						'2017',
						'2018',
						'2019',
						'2020',
						'2021',
						'2022',
						'2023',
						'2024',
						'2025'
					]

					def days  = [
						'01',
						'02',
						'03',
						'04',
						'05',
						'06',
						'07',
						'08',
						'09',
						'10',
						'11',
						'12',
						'13',
						'14',
						'15',
						'16',
						'17',
						'18',
						'19',
						'20',
						'21',
						'22',
						'23',
						'24',
						'25'
					]

					def branches = [:]

					for (def year in years) {
						for (def day in days) {
							def name = "AoC ${year}-${day}"
							def path = "Year${year}/Day${day}"
							def DOCKER_IMAGE = isUnix()
									? DOCKER_IMAGE_LINUX
									: DOCKER_IMAGE_WINDOWS

							if (fileExists(path)) {
								branches[name] = {
									stage(name) {
										dir(path) {
											withDockerContainer(image: DOCKER_IMAGE, args: DOCKER_ARGS) {
												catchError(buildResult: 'FAILURE', stageResult: 'FAILURE', catchInterruptions: false) {
													callShell 'dotnet build'
													callShell 'dotnet test --logger junit'
													junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
													callShell 'dotnet run'
												}
											}
										}
									}
								}
							}
						}
					}

					if (useMultithreading) {
						parallel branches
					} else {
						for (def branch in branches.values()) {
							branch()
						}
					}
				}
			}
		}
	}
}