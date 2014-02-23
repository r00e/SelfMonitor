var ruleForNameAndBook = /^(\w|\d)+$/;
var ruleForPage = /^\d+$/;

var warningMsgForName = "For Name value, only character and number are allowed...";
var warningMsgForBook = "For Book value, only character and number are allowed...";
var warningMsgForPage = "For Page value, only number are allowed...";

function ValidateNew(val, type) {
    switch (type) {
        case "name":
            if (!ruleForNameAndBook.test(val)) {
                alert(warningMsgForName);
                return false;
            } else {
                return true;
            }
        case "book":
            if (!ruleForNameAndBook.test(val)) {
                alert(warningMsgForBook);
                return false;
            } else {
                return true;
            }
        case "page":
            if (!ruleForPage.test(val)) {
                alert(warningMsgForPage);
                return false;
            } else {
                return true;
            }
    }
}

function ValidateForm() {
    var nameFormat = ValidateNew($("#name").val(), "name");
    var bookFormat = ValidateNew($("#book").val(), "book");
    var pageFormat = ValidateNew($("#page").val(), "page");

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

            if (!ValidateNew(newContent, originalClass)) {
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