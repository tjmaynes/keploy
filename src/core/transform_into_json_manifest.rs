use crate::core::keploy_args::{KeployArgs};
use crate::core::read_manifest_json::{read_manifest_json, JsonManifestError};

pub fn transform_into_json_manifest(keploy_args: KeployArgs) -> Result<(), String> {
    match read_manifest_json(keploy_args.file) {
        Ok(_) => { Ok(()) }
        Err(read_error) => {
            match read_error {
                JsonManifestError::MalformedJson => Err("Malformed json!".to_string())
            }
        }
    }
}