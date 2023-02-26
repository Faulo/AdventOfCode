pipeline {
	agent any

	stages {
		stage('Year 2022') {
			environment {
				SOLUTION = 'AdventOfCode.Year2022'
			}
			parallel {
				stage('Day 01') {
					environment {
						PROJECT = 'Day01'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
				stage('Day 02') {
					environment {
						PROJECT = 'Day02'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
				stage('Day 03') {
					environment {
						PROJECT = 'Day03'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
				stage('Day 04') {
					environment {
						PROJECT = 'Day04'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
				stage('Day 05') {
					environment {
						PROJECT = 'Day05'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
				stage('Day 06') {
					environment {
						PROJECT = 'Day06'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
				stage('Day 07') {
					environment {
						PROJECT = 'Day07'
					}
					stages {
						stage('Build') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet build'
								}
							}
						}
						stage('Test') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									sh(script: 'dotnet test --logger junit', returnStatus: true)
									junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
								}
							}
						}
						stage('Run') {
							steps {
								dir("${env.SOLUTION}/${env.PROJECT}") {
									callShell 'dotnet run'
								}
							}
						}
					}
				}
			}
		}
	}
}