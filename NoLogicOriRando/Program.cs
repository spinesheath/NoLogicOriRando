using System.Text.RegularExpressions;

if (!File.Exists("randomizer.dat"))
{
  Console.WriteLine("Place randomizer.dat next to this app.");
  Console.ReadKey();
  return;
}

var file = File.ReadAllLines("randomizer.dat");

var random = new Random((int)DateTime.UtcNow.Ticks);

var pickupRegex = new Regex(@"^([\d-]*\|)(.*)(\|\w*)$");

var pickups = new List<string>();

for (var i = 1; i < file.Length; i++)
{
  var line = file[i];
  var match = pickupRegex.Match(line);
  pickups.Add(match.Groups[2].Value);
}

var shuffled = pickups.OrderBy(_ => random.Next()).ToList();

for (var i = 1; i < file.Length; i++)
{
  var line = file[i];
  var match = pickupRegex.Match(line);
  var replaced = match.Groups[1] + shuffled[i - 1] + match.Groups[3];
  file[i] = replaced;
}

File.WriteAllLines("randomizer_nologic.dat", file);

Console.WriteLine("done");
