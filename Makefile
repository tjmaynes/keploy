install_dependencies:
	cargo install --path .

test: install_dependencies
	cargo test

deploy:
	@ echo "Coming soon!"
