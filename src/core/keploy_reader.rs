use crate::core::keploy_error::KeployError;
use crate::core::keploy_args::KeployArgs;
use std::fs::read_to_string;

pub struct KeployReader {}

pub trait KeyployFileReader {
    fn read_keployrc_file(args: KeployArgs) -> Result<String, KeployError>;
}

impl KeyployFileReader for KeployReader {
    fn read_keployrc_file(args: KeployArgs) -> Result<String, KeployError> {
        match read_to_string(args.file) {
            Ok(result) => Ok(result),
            Err(error) => Err(KeployError::Unknown(error.to_string()))
        }
    }
}