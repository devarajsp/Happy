questionnumber=1;
totalquestion=15;
correctanswers=0;
counter=1;
totalCount=0;


/******Prevent inspect element*******/
$( document ).ready(function() {
	
$(document).bind("contextmenu",function(e) {
 e.preventDefault();
});
$(document).keydown(function(e){
    if(e.which === 123){
       return false;
    }
});
});


/**********/
var resultQuiz= [];
var Topic , resultBollean, Correct_Wrong =false;
 var images = ["bg1.jpg", "bg2.jpg", "bg3.jpg", "bg4.jpg","bg5.jpg","bg6.jpg","bg7.jpg"];
        $(function () {
            var i = 0;
            $("body").css("background-image", "url(images/" + images[i] + ")");
            setInterval(function () {
                i++;
                if (i == images.length) {
                    i = 0;
                }
                
                    $("body").css("background-image", "url(images/" + images[i] + ")");
                    $("body").fadeIn("slow");
               
            }, 5000);
        });

function showsmiley(t,quizname){
	Topic= quizname;

	//alert("t.id"+quizname);
	var GetId=t.id.split("_");
	var smileyID=GetId[0]+"_"+GetId[1]+"_img";
	//alert("t.dg="+t.id)	;
	if(GetId[2]=='correct'){
	    Correct_Wrong =true;
		$("#"+t.id).css("background-color","#009688");
		correctanswers++;
	}
	if(GetId[2]=='wrong'){
	    Correct_Wrong =false;
		var elements = document.getElementsByName( GetId[0]+"_correct" );
		var correctoptionid = elements[0].getAttribute( 'id' );
		//alert(id);
		$("#"+t.id).css("background-color","#E62448");
		$("#"+correctoptionid).css("background-color","#009688");
		$("#"+correctoptionid).css("cursor","not-allowed");
		$("#"+correctoptionid).css("pointer-events","none");
		splitid=correctoptionid.split("_");
		$("#"+splitid[0]+"_"+splitid[1]+"_img").css("display","inline-block");
		$("#"+splitid[0]+"_"+splitid[1]+"_img").css("visibility","visible");
	}
	$("#"+smileyID).css("display","inline-block");
	$("#"+smileyID).css("visibility","visible");
}
function reloadpage(){
	location.reload();
}
function next(){
//alert("next");
resultQuiz.push({
                    "Name": "Guest!..",
                    "HATopic": Topic,
                    "Result":Correct_Wrong,
                     });
totalCount++;
if(totalCount==totalquestion){
//alert(totalCount);
var gh=JSON.stringify(resultQuiz);

    $.ajax({
                   type: "POST",
                   contentType: "application/json",
                   url: "http://happyservice.azurewebsites.net/api/HAQDetails/InsertHAQDetailsList",
                   data:JSON.stringify(resultQuiz),
                   success: function (response) {
                      //  alert("Success");

                   },
                   dataType: "json"//set to JSON
               });
             //  alert("ajax done");
}


	$("#Q"+questionnumber).slideUp( "slow" );
	$("#Q"+(questionnumber+1)).slideDown( "slow" );
	questionnumber++;
	if(questionnumber>totalquestion){
		$("#next").slideUp( "slow" );
		$("#scorediv").slideDown( "slow" );
		$("#refreshicon").css("display","none");
		if(correctanswers==totalquestion){
			//alert("Done");
			$(".resultsmileey").css("display","none");
			$("#awesome").css("display","block");
			$("#score").text( "Perfect Score "+correctanswers+" out of "+totalquestion+"..!!" );
		}
		if(correctanswers<15 && correctanswers>11){
			$(".resultsmileey").css("display","none");
			$("#good").css("display","block");				
			$("#score").text( "Great!! Your Score is "+correctanswers+" out of "+totalquestion+".." );
		}
		if(correctanswers<11 && correctanswers>6){
			$(".resultsmileey").css("display","none");
			$("#good").css("display","block");				
			$("#score").text( "Good!! Your Score is "+correctanswers+" out of "+totalquestion+".." );
		}
		if(correctanswers<6){
			$(".resultsmileey").css("display","none");
			$("#sad").css("display","block");				
			$("#score").text( "Can do better!! Your Score is "+correctanswers+" out of "+totalquestion+".." );
		}
		
	}


}