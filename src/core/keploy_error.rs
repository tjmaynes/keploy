use std::fmt;

pub enum KeployError {
    MalformedJson,
    Unknown(String)
}

impl fmt::Display for KeployError {
    fn fmt(&self, formatter: &mut fmt::Formatter) -> fmt::Result {
        formatter.write_fmt(format_args!("JsonManifestError(error=\"{}\")", self))
    }
}