# Job Application

## Data definition

```json
{
    "cv": "",
    "cover-letter": "",
    "jobDetail": {
        "source": "",
        "title": "",
        "company": "",
        "description": ""
    },
    "notes": []
}
```

```c#
public class JobApplication {
    public string CoverLetter { get; set; }
    public string CV { get; set; }
    public Job JobDetail { get; set; }
    public Note[] Notes { get; set; }
}
```

## What it does?

* Display job details
* Display / make notes
* Display / edit cover letter
* Display / edit CV
