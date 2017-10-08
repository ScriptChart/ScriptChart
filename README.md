### What does this project good for


Imagine you work in your script console of choice. You have acquired some object from e.g. your monitoring system, which contains series of data you want to visualize. This solution allows you to use a webservice where you post your data to and provide a JSONPath to values of your interest. The webservice immediately returns you a link to your visualized data.

### Usage example

Let's say we use PowerShell. We generate some random array of 100 numbers in range from 0 to 50:

```powershell
$randNum = Get-Random -Count 100 -InputObject (0..50)
```

Now let's create a Json object to store this data:

```powershell
$list = New-Object System.Collections.ArrayList 
foreach ($elem in $randNum) {$object = New-Object -TypeName psobject; $object | Add-Member -MemberType NoteProperty -Name num -Value $elem; $list.Add($object)} 
```

Convert it to Json:

```powershell
$json = $list | ConvertTo-Json
```

This is how our resulting Json object look like:

```json
[
  {
      "num":  38
  },
  {
      "num":  1
  }
]
```

We need to specify JsonPath in HTTP header for data we want the chart for:

```powershell
$headers = @{}
$headers.Add("Content-Type", 'application/json')
$headers.Add("X-JPATH-FOR-Y", '$..num')
```
and just submit it:

```powershell
$resp = Invoke-RestMethod -Method Post -Body $json -Headers $headers -Uri http://alexp.tech/api/linechart
```
In the response you will get your chart id. Navigate to [ScriptChart service](https://alexp.tech:8080), enter your chart id, and click submit button. [This is how your chart will look like](http://alexp.tech:8080/8R108zTkLm9Lmy02zg)


![](https://alexproj.visualstudio.com/_apis/public/build/definitions/7455326a-b67c-421b-aabd-d763c6ae6114/5/badge)

Join the [discussion on gitter](https://gitter.im/ScriptChart)
