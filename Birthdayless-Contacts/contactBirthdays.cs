using System.Text.RegularExpressions;

class contactBirthdays {
    private string vcfLocation;
    private string contactData;
    private List<string> people;
    public contactBirthdays(string vcfLocation)
    {
        this.vcfLocation = vcfLocation;
        contactData = getRawContactData();
        people = findBDaylessPeople(contactData, false);
    }
    public contactBirthdays()
    {
        this.vcfLocation = @"/Users/aarian/Desktop/CONTACTS.vcf";
        contactData = getRawContactData();
        people = findBDaylessPeople(contactData, false);
    }
    private string getRawContactData()
    {
        StreamReader readRawContacts = new StreamReader(vcfLocation);
        return (readRawContacts.ReadToEnd());
    }
    public void rmImageData(bool makeFile)
    {
        Random random = new Random();
        string searchIMGDATA = @"(PHOTO;ENCODING=b;TYPE=JPEG:[^:]+)";
        Regex regex = new Regex(searchIMGDATA);
        string newContactData = regex.Replace(contactData, "END");
        if (makeFile)
        {
            StreamWriter write = new StreamWriter(vcfLocation, false);
            write.Write(newContactData);
            write.Close();
        };
    }
    public List<string> findBDaylessPeople(string contacts, bool makeFile)
    {
        Random random = new Random();
        string searchBDayless = @"FN:(.*)(?:\n.*){0,5}(?:\nTEL.*\n)(?:END)";
        Regex regex = new Regex(searchBDayless);
        List<string> people = new List<string>();
        foreach (Match m in regex.Matches(contacts))
        {
            people.Add(m.Groups[1].Value);
        }
        if (makeFile)
        {
            StreamWriter writetwo = new StreamWriter("/Users/aarian/Desktop/peopleList" + random.Next(100) + ".txt", false);
            writetwo.Write(people);
            writetwo.Close();
        }
        return people;
    }
    public void showPeople()
    {
        foreach (string person in people)
        {
            Console.WriteLine(person);
        }
    }
}
