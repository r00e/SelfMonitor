var ruleForNameAndBook = /^(\w|\d)+$/;
var ruleForPage = /^\d+$/;

var warningMsgForName = "For Name value, only character and number are allowed...";
var warningMsgForBook = "For Book value, only character and number are allowed...";
var warningMsgForPage = "For Page value, only number are allowed...";

function Validate(val, format, str) {
    if (!format.test(val)) {
        alert(str);
        return false;
    }

    return true;
}

function ValidateForm() {
    var nameFormat = Validate($("#name").val(), ruleForNameAndBook, warningMsgForName);
    var bookFormat = Validate($("#book").val(), ruleForNameAndBook, warningMsgForBook);
    var pageFormat = Validate($("#page").val(), ruleForPage, warningMsgForPage);

    return nameFormat && bookFormat && pageFormat;
}

function Edit() {
    var me = $(this);
    var originalContent = $(this).text();
    var originalClass = $(this).attr("class");
    var id = $(this).parent().attr("id");

    $(this).html("<input type='text' value='" + originalContent + "' />");
    $(this).children().first().focus();
    $(this).children().first().keypress(function (e) {
        if (e.which == 13) {
            var newContent = $(this).val();

            if (!Validate(newContent, ruleForNameAndBook, warningMsgForBook)) {
                $(this).parent().text(originalContent);
            } else {
                $.ajax({
                    url: window.location.protocol + '//' + window.location.host + "/home/UpdateRecord",
                    type: "POST",
                    data: { id: id, value: newContent, type: originalClass },
                    success: function () {
                        me.text(newContent);
                    },
                    error: function () {
                        console.log("update error");
                    }
                });
            }
        }
    });
    $(this).children().first().blur(function () {
        $(this).parent().text(originalContent);
    });
}

function Delete() {
    var deleteRow = $(this).parent().parent();

    var idOnPage = $(this).parent().parent().attr("id");

    $.ajax({
        url: window.location.protocol + '//' + window.location.host + "/home/DeleteRecord",
        type: "POST",
        dataType: "json",
        data: { id: idOnPage },
        success: function (result) {
            //                        alert(result);
        },
        error: function () {
            alert("delete error");
        }
    });

    $(deleteRow).remove();
}

$(function() {
    $("td#info").bind("dblclick", Edit);
    $("td #delete").bind("click", Delete);
});