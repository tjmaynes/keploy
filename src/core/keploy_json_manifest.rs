use serde::{Deserialize, Serialize};
use std::fmt;
use serde_json::Number;
use std::fmt::Formatter;
use crate::core::keploy_error::KeployError;

#[derive(Serialize, Deserialize)]
pub struct KeployJsonManifest {
    pub name: String,
    pub image: String,
    pub port: Number,
}

impl fmt::Display for KeployJsonManifest {
    fn fmt(&self, formatter: &mut Formatter) -> fmt::Result {
        formatter.write_fmt(format_args!("KeployJsonManifest(name=\"{}\", image=\"{}\", port={})", self.name, self.image, self.port))
    }
}

pub trait KeployJsonManifestConverter {
    fn from_raw_json_string(json_string: String) -> Result<KeployJsonManifest, KeployError>;
}

impl KeployJsonManifestConverter for KeployJsonManifest {
    fn from_raw_json_string(json_string: String) -> Result<KeployJsonManifest, KeployError> {
        serde_json::from_str(&*json_string)
            .and_then(|data| Ok(data))
            .map_err(|_| KeployError::MalformedJson)
    }
}

#[cfg(test)]
mod tests {
    use serde_json::Number;
    use crate::core::keploy_error::KeployError;
    use crate::core::keploy_json_manifest::{KeployJsonManifest, KeployJsonManifestConverter};

    #[test]
    fn from_raw_json_string_should_return_json_manifest_when_given_valid_json_string() {
        match KeployJsonManifest::from_raw_json_string(String::from(r#"
        {
            "name": "some-service",
            "image": "docker.io/ddubson/my-website:0.1.0",
            "port": 8080
        }"#)) {
            Ok(result) => {
                assert_eq!(result.name, "some-service");
                assert_eq!(result.image, "docker.io/ddubson/my-website:0.1.0");
                assert_eq!(result.port, Number::from(8080));
            }
            Err(_) => assert!(false, "Should be valid json!")
        }
    }

    #[test]
    fn from_raw_json_string_should_return_malformed_json_when_given_invalid_json_string() {
        match KeployJsonManifest::from_raw_json_string(String::from(r#"
        {
            "name": "some-service"
            "image": "docker.io/ddubson/my-website:0.1.0"
            "port": "8080
        }"#)) {
            Ok(_) => assert!(false, "Should be invalid json!"),
            Err(error) => assert!(matches!(error, KeployError::MalformedJson))
        }
    }
}