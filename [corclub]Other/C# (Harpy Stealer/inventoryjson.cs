using System;
using System.Collections.Generic;

internal class inventoryjson
{
    private string _contextID;
    private List<string> list1 = new List<string>();
    private List<string> list2 = new List<string>();

    private List<string[]> getItems()
    {
        List<string[]> list = new List<string[]>();
        for (int i = 0; i < this.list1.Count; i++)
        {
            string input = this.list1[i];
            string str2 = this.getValue(input, "id");
            string str3 = this.getValue(input, "classid");
            string str4 = string.Empty;
            for (int j = 0; j < this.list2.Count; j++)
            {
                if (this.getValue(this.list2[j], "classid") == str3)
                {
                    str4 = this.list2[j];
                    break;
                }
            }
            if (str4 != string.Empty)
            {
                string[] item = new string[] { this.getValue(str4, "appid"), this.getValue(input, "amount"), str2, this.getValue(str4, "market_name"), this.getValue(str4, "type").ToLower(), this._contextID };
                if (!list.Contains(item) && (this.getValue(str4, "tradable") == "1"))
                {
                    list.Add(item);
                }
            }
        }
        return list;
    }

    private string getValue(string input, string name)
    {
        string[] strArray = input.Split(new char[] { '"', ':' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < strArray.Length; i++)
        {
            if (strArray[i] == name)
            {
                return strArray[i + 1].Replace(",", "");
            }
        }
        return null;
    }

    public List<string[]> Parse(string input, string contextid)
    {
        this._contextID = contextid;
        string[] strArray = input.Split(new char[] { '{', '}' });
        if ((strArray.Length > 2) && (strArray[1].IndexOf("true") != -1))
        {
            this.parserginventory(strArray);
            this.parsergdescrptions(strArray);
            return this.getItems();
        }
        return new List<string[]>();
    }

    private void parsergdescrptions(string[] input)
    {
        int num = -1;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].IndexOf("rgDescriptions") != -1)
            {
                num = i + 1;
                break;
            }
        }
        for (int j = num; j < input.Length; j++)
        {
            string[] strArray = input[j].Split(new char[] { '"' });
            if ((strArray.Length > 2) && (strArray[1] == "appid"))
            {
                this.list2.Add(input[j]);
            }
        }
    }

    private void parserginventory(string[] input)
    {
        int num = -1;
        int num2 = -1;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i].IndexOf("rgInventory") != -1)
            {
                num = i + 1;
            }
            if ((num != -1) && (input[i] == ""))
            {
                num2 = i;
                break;
            }
        }
        for (int j = num; j < num2; j++)
        {
            string[] strArray = input[j].Split(new char[] { '"' });
            if ((strArray.Length > 2) && (strArray[1] == "id"))
            {
                this.list1.Add(input[j]);
            }
        }
    }
}

