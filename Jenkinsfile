def prepare(solution, version) {
	def projects = [
		'Day01',
		'Day02',
		'Day03',
		'Day04',
		'Day05',
		'Day06',
		'Day07',
		'Day08',
		'Day09',
		'Day10',
		'Day11',
		'Day12',
		'Day13',
		'Day14',
		'Day15',
		'Day16',
		'Day17',
		'Day18',
		'Day19',
		'Day20',
		'Day21',
		'Day22',
		'Day23',
		'Day24',
		'Day25'
	]

	return {
		if (fileExists(solution)) {
			dir(solution) {
				def unix = isUnix()
				def tag = unix
						? version
						: version + "-windowsservercore-ltsc2019" // "-nanoserver-1809" "-windowsservercore-ltsc2019"

				withDockerContainer(image: "mcr.microsoft.com/dotnet/sdk:${tag}") {
					stage(solution) {
						callShell 'dotnet restore'
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
	stage(path) {
		callShell 'dotnet build'
		callShell 'dotnet test --logger junit'
		junit(testResults: '**/TestResults.xml', allowEmptyResults: true)
		callShell 'dotnet run'
	}
}

pipeline {
	agent {
		label 'docker'
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
					def branches = [:]

					branches['Year2024'] = prepare('Year2024', '8.0')
					branches['Year2023'] = prepare('Year2023', '8.0')
					branches['Year2022'] = prepare('Year2022', '8.0')

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