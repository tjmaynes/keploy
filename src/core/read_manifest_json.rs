use serde::{Deserialize, Serialize};

#[derive(Serialize, Deserialize)]
pub struct JsonManifest {
    pub name: String,
    pub image: String,
    pub port: String
}

pub enum JsonManifestError {
    MalformedJson
}

pub fn read_manifest_json(json_string: &str) -> Result<JsonManifest, JsonManifestError> {
    serde_json::from_str(json_string)
        .and_then(|data: JsonManifest| Ok(data))
        .map_err(|_| JsonManifestError::MalformedJson)
}

#[cfg(test)]
mod tests {
    use crate::core::read_manifest_json::{read_manifest_json, JsonManifestError};

    #[test]
    fn read_manifest_json_should_return_json_manifest_when_given_valid_json_string() {
        match read_manifest_json(r#"
        {
            "name": "some-service",
            "image": "docker.io/ddubson/my-website:0.1.0",
            "port": "8080"
        }"#) {
            Ok(result) => {
                assert_eq!(result.name, "some-service");
                assert_eq!(result.image, "docker.io/ddubson/my-website:0.1.0");
                assert_eq!(result.port, "8080");
            }
            Err(_) => { assert!(false, "Should be valid json!") }
        }
    }

    #[test]
    fn read_manifest_json_should_return_malformed_json_when_given_invalid_json_string() {
        match read_manifest_json(r#"
        {
            "name": "some-service"
            "image": "docker.io/ddubson/my-website:0.1.0"
            "port": "8080
        }"#) {
            Ok(_) => { assert!(false, "Should be invalid json!") }
            Err(error) => { assert!(matches!(error, JsonManifestError::MalformedJson)) }
        }
    }
}