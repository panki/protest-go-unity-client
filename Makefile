release:
	npm version patch -m "v%s"
	git push --tags
