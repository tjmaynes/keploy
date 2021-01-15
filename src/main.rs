mod core;

use crate::core::keploy_args::{KeployArgs};

fn main() -> Result<(), String> {
    KeployArgs::new()
        .and_then(|keploy_args| {
            println!("{}", keploy_args);
            Ok(())
        })
}