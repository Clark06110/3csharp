using var client = new HttpClient();

try
{
    var url = "https://localhost:7190/api/Classrooms/Async";

    var response = await client.GetAsync(url);

    response.EnsureSuccessStatusCode();

    var responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine(responseBody);
    Console.ReadLine();
}
catch (HttpRequestException e)
{
    Console.WriteLine($"Erreur de requête : {e.Message}");
}