using System.Text;
using UnityEngine;
using System.Collections.Generic;

//custom extension
using ExtensionTool;


class HtmlElement
{
    public string Name, Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int indentSize = 2;

    public HtmlElement()
    {

    }

    public HtmlElement(string name, string text)
    {
        Name = name;
        Text = text;
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', indentSize * indent);
        sb.AppendLine(string.Format("{0}<{1}>", i, Name));
        if (!string.IsNullOrEmpty(Text))
        {
            sb.Append(new string(' ', indentSize* (indent +1)));
            sb.Append(Text);
            sb.AppendLine();
        }

        foreach (var e in Elements)
        {
            sb.Append(e.ToStringImpl(indent+1));
        }

        sb.AppendLine(string.Format("{0}</{1}>",i,Name));
        return sb.ToString();
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }
}

class HtmlBuilder
{
    private readonly string rootName;
    HtmlElement root = new HtmlElement();
    public HtmlBuilder(string rootName)
    {
        this.rootName = rootName;
        root.Name = rootName;
    }

    public void AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName,childText);
        root.Elements.Add(e);
    }

    public HtmlBuilder AddChildFluent(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        root.Elements.Add(e);
        return this;
    }

    public override string ToString()
    {
        return root.ToString();
    }

    public void Clear()
    {
        root= new HtmlElement{Name =rootName};
    }
}


public class Builder : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
    
        var hello = "hello";
        var sb= new StringBuilder();
        sb.Append("<p>");
        sb.Append(hello);
        sb.Append("</p>");
        Debug.Log(sb);

        var words = new[] {"hello","world"};
        //clear
        sb.Clear();//<-this is from extension method
        //string builder Clear(); only available .net 4.0 or higher

        sb.Append("<ul>");
        foreach (var word in words)
        {
            sb.AppendFormat("<li>{0}</li>",word);
        }
        sb.Append("</ul>");
        Debug.Log(sb);

        var builder= new HtmlBuilder("ul");
        builder.AddChild("li","hello");
        builder.AddChild("li","world");
        Debug.Log(builder.ToString());

        sb.Clear();
        builder.Clear();
        builder.AddChildFluent("li", "bye").AddChildFluent("li", "bubu").AddChildFluent("meme",null);
        Debug.Log(builder);


    }


}

