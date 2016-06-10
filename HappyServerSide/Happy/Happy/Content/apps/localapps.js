function AddMenuItem(titleTxt, hrefTxt, colorTxt, buttonSize, iconTxt, styleTxt, colTxt )
{
    var classTxt = ' contact-box ';
    var classColor = 'color-10';
    var menuHref = "#PesonalizeHappy";
    var menuTxt = "Personalize HAPPY";
    var menuIcon = "fa-magic";
    var column = "2";
    var bigButton = "twelve small-12";
    var smallButton = "six small-6";
    var buttonStyle = bigButton;

    if (titleTxt != null)
    {
        menuTxt = titleTxt;
    }
    if (hrefTxt != null)
    {
        menuHref = hrefTxt ;
    }
    if (iconTxt != null)
    {
        menuIcon = iconTxt;
    }
    if (colorTxt != null)
    {
        classColor = colorTxt;
    }
    if (colTxt != null)
    {
        column = colTxt;
    }
    if (buttonSize == "small")
    {
        buttonStyle = smallButton;
    }


    var menuItem =
        "<div class='"+buttonStyle+" columns " + classTxt + "space'>" +
            "<div class='"+classColor+"'>" +
                "<a href='"+ menuHref +"'> " +
                    "<span class='box-title'>"+ menuTxt +"</span>" +
                    "<br />" +
                    "<i class='"+ menuIcon+ " fa fa-4x'></i>" +
                "</a>" +
            "</div>" +
        "</div>";

      
     var existingHtml = $("#menucolumn"+column).html();
     
	 var newHtml = existingHtml + menuItem;

	 $("#menucolumn" + column).html(newHtml);

}

function IsValidRole(role, rTags)
{
    var resVal = false;
     
    for (var str in rTags)
    {
        //alert(str);
        if ( role == rTags[str] )
        {
            resVal = true;
            break;
        }
    }

    return resVal;
}