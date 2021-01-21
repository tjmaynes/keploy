use std::fmt::Display;

pub fn finish_program<T: Display, E: Display>(results: Result<T, E>) -> Result<(), String> {
    match results {
        Ok(results) => {
            println!("Success: {}", results.to_string());
            Ok(())
        },
        Err(error) => {
            println!("Error: {}", error.to_string());
            Err(error.to_string())
        }
    }
}

// use std::fmt::Display;

// pub trait LogResult<T, E> {
//     fn safe_print(&self) -> Result<T, E>;
// }
//
// impl<T: Display, E: Display> LogResult<T, E> for Result<T, E> {
//     fn safe_print(&self) -> Result<T, E> {
//         let cloned_self = Result::cloned(self);
//
//         match cloned_self {
//             Ok(result) => print!("{}", result.to_string()),
//             Err(error) => print!("{}", error.to_string())
//         };
//
//         cloned_self
//     }
// }