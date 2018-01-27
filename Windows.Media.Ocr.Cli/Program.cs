using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using Windows.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Windows.Media.Ocr.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            args = new string[] { "..\\..\\x.png" };
#endif
            string imagePath = null;
            string language = "zh-Hans-CN";
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg == "-s")
                {
                    Console.WriteLine("All of supported language");
                    foreach (var lang in OcrEngine.AvailableRecognizerLanguages)
                    {
                        Console.WriteLine(lang.LanguageTag);
                    }
                    break;
                }
                if (arg == "-h" || arg == "--help")
                {
                    PrintHelp();
                    break;
                }
                if (arg == "-l" && i < args.Length - 1)
                {
                    language = args[i + 1];
                    i++;
                    continue;
                }
                imagePath = arg;
                try
                {
                    var result = RecognizeAsync(imagePath, language).GetAwaiter().GetResult();
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
#if DEBUG
            Console.ReadLine();
#endif
        }

        static void PrintHelp()
        {
            Console.WriteLine(@"Usage: Windows.Media.Ocr-CLI.exe [options...] <image file path>
Example: Windows.Media.Ocr.Cli.exe x.png
-l      <language>  Default:zh-Hans-CN   Specify language to reconizing
-s      Show all supported languages
-h      Show help like this
");
        }

        static async Task<string> RecognizeAsync(string imagePath, string language)
        {
            StorageFile storageFile;
            var path = Path.GetFullPath(imagePath); // x.png
            var extName = Path.GetExtension(path); // .png
            var outPath = path.Replace(extName, "") + "-out" + extName;  // x-out.png
            storageFile = await StorageFile.GetFileFromPathAsync(path);
            IRandomAccessStream randomAccessStream = await storageFile.OpenReadAsync();
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(randomAccessStream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            Globalization.Language lang = new Globalization.Language(language);            

            string result = null;
            if (OcrEngine.IsLanguageSupported(lang))
            {
                OcrEngine engine = OcrEngine.TryCreateFromLanguage(lang);
                if (engine != null)
                {
                    OcrResult ocrResult = await engine.RecognizeAsync(softwareBitmap);
                    foreach (var tempLine in ocrResult.Lines)
                    {
                        string line = "";
                        foreach (var word in tempLine.Words)
                        {
                            line += word.Text;
                        }
                        result += line + "\n";
                    }
                }
            }
            else
            {
                throw new Exception(string.Format("Language {0} is not supported", language));
            };
            randomAccessStream.Dispose();
            softwareBitmap.Dispose();
            return await Task<string>.Run(() =>
            {
                return result;
            });
        }
    }
}
