open System

/// Read a valid integer (between 1 and 10) from key input
let readint i =     // Meaningless function parameter, just there to make function work!
    let mutable continueLooping = true
    let mutable num = 0
    while continueLooping do
        Console.WriteLine("Enter number of people (between 1 and 10): ")
        let canparse, keyin = Int32.TryParse(Console.ReadLine())
        num <- keyin
        if canparse && keyin > 0 && keyin < 11 then continueLooping <- false
    num

/// Read a valid age (between 0 and 150) from key input
let readage (person: string) =
    let mutable continueLooping = true
    let mutable age = 0
    while continueLooping do
        Console.WriteLine("Enter {0}'s age (between 0 and 150): ", person)
        let canparse, keyin = Int32.TryParse(Console.ReadLine())
        age <- keyin
        if canparse && age >= 0 && age < 151 then continueLooping <- false
    age

/// Check console input is an integer, otherwise try again or return zero
let checkvalue (argv : string []) : int =
    if argv.Length > 0 then
        let couldparse, consolein = Int32.TryParse(argv.[0])
        if couldparse then consolein
        else
            let canparse, keyin = Int32.TryParse(Console.ReadLine())
            if canparse then keyin
            else 0
    else
        let canparse, keyin = Int32.TryParse(Console.ReadLine())
        if canparse then keyin
        else 0

/// Given person's name & age, return a string based on their age range
let showagerange name age =
    if age < 13 then
        sprintf "%s is a kid or child." name
    elif age >= 20 then
        sprintf "%s is no longer a teenager." name
    else
        sprintf "%s is a teenager." name

[<EntryPoint>]    
let main argv =
    let mutable input = checkvalue argv

    // get correct input if initial value is wrong
    if input < 1 || input > 10 then
        input <- readint 0

    let mutable name = ""
    let mutable age = 0
    // loop once for each person
    for i = 1 to input do
        Console.WriteLine("Enter person's name: ")
        name <- Console.ReadLine()
        age <- readage name
        let op = showagerange name age
        Console.WriteLine(op)

    // prompt to terminate program by typing a key
    Console.WriteLine("Type a key to terminate program.")
    Console.ReadKey()

    0 // return an integer exit code
