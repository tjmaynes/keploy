install_dependencies:
	cargo install --path .

test: install_dependencies
	cargo $@ 

build:
	cargo $@

run:
	cargo $@ 

deploy:
	@ echo "Coming soon!"
