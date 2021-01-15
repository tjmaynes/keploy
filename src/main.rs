mod core;

use crate::core::keploy_args::KeployArgs;
use crate::core::transform_into_json_manifest::transform_into_json_manifest;

fn main() -> Result<(), String> {
    KeployArgs::new()
        .and_then(transform_into_json_manifest)
}