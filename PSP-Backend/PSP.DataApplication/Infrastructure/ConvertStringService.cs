namespace PSP.DataApplication.Infrastructure;

public static class ConvertStringService
{
    public static string Transliterate(string text)
    {
        var transliterationTable = new Dictionary<char, char>
        {
            {'а', 'a'}, {'б', 'b'}, {'в', 'v'}, {'г', 'g'}, {'д', 'd'}, {'е', 'e'}, {'ё', 'e'}, {'ж', 'z'},
            {'з', 'z'}, {'и', 'i'}, {'й', 'y'}, {'к', 'k'}, {'л', 'l'}, {'м', 'm'}, {'н', 'n'}, {'о', 'o'},
            {'п', 'p'}, {'р', 'r'}, {'с', 's'}, {'т', 't'}, {'у', 'u'}, {'ф', 'f'}, {'х', 'k'}, {'ц', 't'},
            {'ч', 'c'}, {'ш', 's'}, {'щ', 's'}, {'ъ', '-'}, {'ы', 'y'}, {'ь', '-'}, {'э', 'e'}, {'ю', 'y'},
            {'я', 'y'},
            {'А', 'A'}, {'Б', 'B'}, {'В', 'V'}, {'Г', 'G'}, {'Д', 'D'}, {'Е', 'E'}, {'Ё', 'E'}, {'Ж', 'Z'},
            {'З', 'Z'}, {'И', 'I'}, {'Й', 'Y'}, {'К', 'K'}, {'Л', 'L'}, {'М', 'M'}, {'Н', 'N'}, {'О', 'O'},
            {'П', 'P'}, {'Р', 'R'}, {'С', 'S'}, {'Т', 'T'}, {'У', 'U'}, {'Ф', 'F'}, {'Х', 'K'}, {'Ц', 'T'},
            {'Ч', 'C'}, {'Ш', 'S'}, {'Щ', 'S'}, {'Ъ', '-'}, {'Ы', 'Y'}, {'Ь', '-'}, {'Э', 'E'}, {'Ю', 'Y'},
            {'Я', 'Y'}
        };

        var str = "";
        foreach (var ch in text)
        {
            str = text.Replace(ch, transliterationTable.TryGetValue(ch, out var value)?value:ch);
        }

        return str;
    }
}