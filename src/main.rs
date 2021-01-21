mod core;
mod utils;

use crate::utils::finish_program;
use crate::core::keploy_args::KeployArgs;
use crate::core::keploy_reader::{KeployReader, KeyployFileReader};
use crate::core::keploy_json_manifest::{KeployJsonManifest, KeployJsonManifestConverter};

fn main() -> Result<(), String> {
    let result = KeployArgs::new()
        .and_then(KeployReader::read_keployrc_file)
        .and_then(KeployJsonManifest::from_raw_json_string);
        // .safe_print();

    finish_program(result)
}