use clap::{Arg, App};
use std::ffi::OsString;
use std::fmt;

#[derive(Debug, PartialEq)]
pub struct KeployArgs {
    file: String
}

impl fmt::Display for KeployArgs {
    // This trait requires `fmt` with this exact signature.
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        write!(f, "{}", self.file)
    }
}

impl KeployArgs {
    pub fn new() -> Result<Self, String> {
        Self::new_from(std::env::args_os().into_iter())
    }

    fn new_from<I, T>(args: I) -> Result<Self, String>
        where
            I: Iterator<Item = T>,
            T: Into<OsString> + Clone, {
        let app = App::new("Keploy")
            .version("0.1.0")
            .author("some-authors")
            .about("some-description")
            .arg(Arg::with_name("file")
                .short("f")
                .takes_value(true)
                .index(1)
                .help("File path of the keploy manifest file")
                .default_value(".keployrc.json"));

        match app.get_matches_from_safe(args) {
            Ok(result) => {
                let file = result
                    .value_of("file")
            .       expect("This can't be None, we said it was required");

                Ok(KeployArgs { file: file.to_string() })
            }
            Err(error) => { Err(error.to_string()) }
        }
    }
}

#[cfg(test)]
mod tests {
    use crate::core::keploy_args::KeployArgs;

    #[test]
    fn test_with_no_file_argument_should_use_default_file() {
        match KeployArgs::new_from([""].iter()) {
            Ok(result) => { assert_eq!(result, KeployArgs {
                file: ".keployrc.json".to_string()
            })}
            Err(_) => { assert!(false, "Should be valid KeployArgs!")  }
        }
    }

    #[test]
    fn test_with_given_file_overrides_default_file() {
        match KeployArgs::new_from(["file", "another-file.json"].iter()) {
            Ok(result) => { assert_eq!(result, KeployArgs {
                file: "another-file.json".to_string()
            })}
            Err(_) => { assert!(false, "Should be valid KeployArgs!")  }
        }
    }
}