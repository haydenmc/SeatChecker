# SeatChecker
SeatChecker is a quick .NET Core program used to query the [Purdue.io API](https://github.com/Purdue-io/PurdueApi) at a regular interval and check for open seats given a list of CRNs.

## Getting Started
1. Install the [.NET Core SDK](https://www.microsoft.com/net/core)
2. Clone this repository
3. Copy `appsettings.default.json` to `appsettings.json` and change the values to your liking.
4. `dotnet restore`, `dotnet run`

## Configuration Values
- `MyPurdueUser`: MyPurdue username
- `MyPurduePass`: MyPurdue password
- `CheckIntervalSeconds`: Number of seconds between queries for seat count
- `TermCode`: MyPurdue term code for the term in which to query CRNs (e.g. 201710 for Fall 2016). You can [query Purdue.io for term codes](https://api.purdue.io/odata/Terms).
- `Crns`: An array of CRNs to check

## Next Steps...
- Notify when there are seats available (Email, SMS, Tweet...)
- Use [authenticated APIs](https://github.com/Purdue-io/PurdueApi/blob/2945ab719043a3fdab77edb1759c73610b48e963/Purdue.io%20API/Controllers/StudentController.cs#L120) to automatically register when a seat opens...
