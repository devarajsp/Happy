<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Samplesol.aspx.cs" Inherits="TechColaApp.Samplesol" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html>
<html>
<head>
    <title>Buddha Laughs</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <link href="http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,300italic"
        rel="stylesheet" type="text/css" />
    <!--[if lte IE 8]><script src="css/ie/html5shiv.js"></script><![endif]-->
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.poptrox.min.js"></script>
    <script src="js/skel.min.js"></script>
    <script src="js/init.js"></script>
    <link rel="stylesheet" href="js/jquery-ui.css">
    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/jquery-ui.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <noscript>
        <link rel="stylesheet" href="css/skel-noscript.css" />
        <link rel="stylesheet" href="css/style.css" />
    </noscript>
    <script type="text/javascript">

        $(function () {
            $("#first").hide();

        })
        $(function () {
            $("#origin_autocomplete").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "Samplesol.aspx/getChemicalName",
                        dataType: "json",
                        data: "{ searchterm: '" + request.term + "' }",
                        type: "POST",
                        cache: false,
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            $.each(data, function (index, element) {
                                response($.map($.parseJSON(element), function (item) {
                                    return {
                                        value: item.Id,
                                        label: item.Name
                                    };
                                }))
                            });
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(errorThrown);
                        }
                    });
                },
                select: function (event, ui) {
                    //alert("Here");
                    event.preventDefault();
                    $("#hdnFrom").val(ui.item.value);
                    //$("#MainContent_origin_autocomplete").val(ui.item.label);
                    setTimeout(function () {
                    }, 1);
                },
                focus: function (event, ui) { event.preventDefault(); $("#origin_autocomplete").val(ui.item.label); },
                change: function (event, ui) { $("#origin_autocomplete").val(ui.item.label); },
                minLength: 1
            });
        });

        $(function () {
            $(document).tooltip({
                track: true
            });
        });

        //        $(function () {
        //            var scrollSpeed = 1000;
        //            $("#scrollchem").click(function () {
        //                $('html, body').animate({ scrollTop: $("#banner").offset().top }, scrollSpeed);
        //            });
        //        });

        $(function () {
            $("#btnClear").click(function () {
                $.ajax({
                    url: "Samplesol.aspx/clearResults",
                    success: function () {
                        var scrollSpeed = 1000;
                        $('html, body').animate({ scrollTop: 0 }, scrollSpeed);
                        $("#origin_autocomplete").val("");
                        $("#hdnFrom").val("");
                        document.getElementById('Slider').innerHTML = "";
                        $("#first").hide();

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });
            });
        });
        $(function () {
            $("#hrefddd").click(function () {
                //alert("3");
                var x = $("#hdnFrom").val();
                $("#hdnFrom").val("");
                

                //alert(x);
                $.ajax({
                    url: "Samplesol.aspx/getChemicalData",
                    dataType: "json",
                    data: "{ id: '" + x + "' }",
                    type: "POST",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var returnedstring = (data.d);
                        if (returnedstring != "") {
                            $("#first").show();
                            var jsonData = $.parseJSON(data.d);
                            var titleH = "";
                            var titleF = "";
                            var titleR = "";
                            var titleS = "";
                            var rectH = "";
                            var rectF = "";
                            var rectR = "";
                            var rectS = "";
                            $.each(jsonData, function (k, v) {
                                if (k == "Health Hazard")
                                    titleH = "Title = " + '"Health Hazard : ' + v + '"';
                                else if (k == "Fire Hazard")
                                    titleF = "Title = " + '"Fire Hazard : ' + v + '"';
                                else if (k == "Reactivity Hazard")
                                    titleR = "Title = " + '"Reactivity Hazard : ' + v + '"';
                                else if (k == "Special Hazard")
                                    titleS = "Title = " + '"Special Hazard : ' + v + '"';
                                else if (k == "Health Hazard Code") {
                                    if (v == "-1")
                                    //rectH = rectH + "<canvas width=\"20%\" height=\"20%\" style=\"background-color: #e3e3e3;border:1px solid #000000;\"" + titleH + "></canvas>";
                                        rectH = rectH + "<canvas class=\"level-1\"" + titleH + "></canvas>";
                                    else if (v == "0")
                                        rectH = rectH + "<canvas class=\"level0\"" + titleH + "></canvas>";
                                    else if (v == "1")
                                        rectH = rectH + "<canvas class=\"level1\"" + titleH + "></canvas>";
                                    else if (v == "2")
                                        rectH = rectH + "<canvas class=\"level2\"" + titleH + "></canvas>";
                                    else if (v == "3")
                                        rectH = rectH + "<canvas class=\"level3\"" + titleH + "></canvas>";
                                    else if (v == "4")
                                        rectH = rectH + "<canvas class=\"level-4\"" + titleH + "></canvas>";
                                }
                                else if (k == "Fire Hazard Code") {
                                    if (v == "-1")
                                        rectF = rectF + "<canvas class=\"level-1\"" + titleF + "></canvas>";
                                    else if (v == "0")
                                        rectF = rectF + "<canvas class=\"level0\"" + titleF + "></canvas>";
                                    else if (v == "1")
                                        rectF = rectF + "<canvas class=\"level1\"" + titleF + "></canvas>";
                                    else if (v == "2")
                                        rectF = rectF + "<canvas class=\"level2\"" + titleH + "></canvas>";
                                    else if (v == "3")
                                        rectF = rectF + "<canvas class=\"level3\"" + titleF + "></canvas>";
                                    else if (v == "4")
                                        rectF = rectF + "<canvas class=\"level4\"" + titleF + "></canvas>";
                                }
                                else if (k == "Reactivity Hazard Code") {
                                    if (v == "-1")
                                        rectR = rectR + "<canvas class=\"level-1\"" + titleR + "></canvas>";
                                    else if (v == "0")
                                        rectR = rectR + "<canvas class=\"level0\"" + titleR + "></canvas>";
                                    else if (v == "1")
                                        rectR = rectR + "<canvas class=\"level1\"" + titleR + "></canvas>";
                                    else if (v == "2")
                                        rectR = rectR + "<canvas class=\"level2\"" + titleR + "></canvas>";
                                    else if (v == "3")
                                        rectR = rectR + "<canvas class=\"level3\"" + titleR + "></canvas>";
                                    else if (v == "4")
                                        rectR = rectR + "<canvas class=\"level4\"" + titleR + "></canvas>";
                                }
                                else if (k == "Special Hazard Code") {
                                    if (v == "-1")
                                        rectS = rectS + "<canvas class=\"level-1\"" + titleS + "></canvas>";
                                    else if (v == "0")
                                        rectS = rectS + "<canvas class=\"level0\"" + titleS + "></canvas>";
                                    else if (v == "1")
                                        rectS = rectS + "<canvas class=\"level1\"" + titleS + "></canvas>";
                                    else if (v == "2")
                                        rectS = rectS + "<canvas class=\"level2\"" + titleS + "></canvas>";
                                    else if (v == "3")
                                        rectS = rectS + "<canvas class=\"level3\"" + titleS + "></canvas>";
                                    else if (v == "4")
                                        rectS = rectS + "<canvas class=\"level4\"" + titleS + "></canvas>";
                                }
                            });
                            var inn = document.getElementById('Slider').innerHTML;
                            var innn = inn + "<h1 " + ">" + rectH + "  " + rectF + "  " + rectR + "  " + rectS + "  " + $("#origin_autocomplete").val() + "</h1>";
                            innn = innn + "<div><table class=\"default\"><thead><tr><th>Property</th><th>Description</th></tr></thead><tbody>";
                            $.each(jsonData, function (k, v) {
                                if (k != "URL" && k != "Health Hazard Code" && k != "Fire Hazard Code" && k != "Reactivity Hazard Code" && k != "Special Hazard Code")
                                    innn = innn + "<tr><td>" + k + "</td><td>" + v + "</td></tr>";
                                else if (k == "URL") {
                                    innn = innn + "<tr><td colspan=\"2\"></td></tr>"
                                    innn = innn + "<tr><td colspan=\"2\">" + "For More Infomation, " + "<a href=\"" + v + "\" Target=\"_blank\" style=\"text-decoration:underline;color: Blue\" >click here</a></td></tr>";
                                }
                            });
                            innn = innn + "</tbody></table></div>";
                            document.getElementById('Slider').innerHTML = "";
                            $("#Slider").removeClass();
                            document.getElementById('Slider').innerHTML = innn;
                            $("#Slider").accordion().accordion("destroy");
                            $("#Slider").accordion({
                                active: false,
                                autoHeight: false,
                                navigation: true,
                                collapsible: true,
                                heightStyle: "content"
                            });
                        }
                        var scrollSpeed = 1000;
                        $('html, body').animate({ scrollTop: $("#first").offset().top }, scrollSpeed);
                        //$.scrollTo("#first", scrollSpeed);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                    }
                });

            });
        });  
         
            
    </script>
    <!--[if lte IE 8]><link rel="stylesheet" href="css/ie/v8.css" /><![endif]-->
</head>
<body>
    <form id="Form2" runat="server">
    <!-- Header -->
    <%--<section id="header">
				<header>
					<h1>Welcome</h1>
                    
				</header>
				<footer>
                 <a href="#" class="button style2 scrolly" id="scrollchem">Search Chemicals</a>
				</footer>
			</section>--%>
    <!-- Banner -->
    <section id="banner">
				<header>
					<h2>Chemical Property Search</h2>
                   <asp:HiddenField ID="hdnFrom" runat="server" />
				</header>
 				<footer>
                    <div>
                        <asp:TextBox ID="origin_autocomplete" placeholder="Search" runat="server" Width="193px"></asp:TextBox>
                    </div>
                    <div>
                        <a href="#" id="hrefddd" class="button style2 scrolly">Search</a>
                    </div>
		       </footer>
			</section>
    <article id="first" class="container box style1 right">
    <ul class="legend">
            <li><canvas class="level4"></canvas>Highly Severe </li>
            <li><canvas class="level3"></canvas>Severe </li>
            <li><canvas class="level2"></canvas>Moderate </li>
            <li><canvas class="level1"></canvas>Less/No Effect </li>
            <li><canvas class="level0"></canvas>No Effect </li>
            <li><canvas class="level-1"></canvas>Not Available </li>
        </ul>
        <div id="Slider">
        
        </div>
        <br />
        <div id="Clear">
        <center>
            <a href="#" id="btnClear" class="button style4 scrolly">Clear</a>
        </center>
        </div>
        <br />
        </article>
    <section id="footer">
			<ul class="icons">
				<li><a href="#" class="fa fa-twitter solo"><span>Twitter</span></a></li>
				<li><a href="#" class="fa fa-facebook solo"><span>Facebook</span></a></li>
				<li><a href="#" class="fa fa-google-plus solo"><span>Google+</span></a></li>
				<li><a href="#" class="fa fa-pinterest solo"><span>Pinterest</span></a></li>
				<li><a href="#" class="fa fa-dribbble solo"><span>Dribbble</span></a></li>
				<li><a href="#" class="fa fa-linkedin solo"><span>LinkedIn</span></a></li>
			</ul>
			<div class="copyright">
				<ul class="menu">
					<li>&copy; All rights reserved.</li>
				</ul>
			</div>
		</section>
    </form>
</body>
</html>
