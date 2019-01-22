function add_budget_list_row(id) {
    var now = new Date;
    var timestamp = now.getUTCDate() + '' + now.getUTCHours() + '' + now.getUTCMinutes() + '' + now.getUTCSeconds() + '' + now.getUTCMilliseconds();

    var add_row = '<tr id="' + timestamp + '"><th scope="row"><input type="text" placeholder="Enter Item" class="form-control input-md" id="' + timestamp + 'row_input_item" ></th><td><input type="text" placeholder="Estimate" class="form-control input-md" id="' + timestamp + 'row_input_estimate"  ></td><td><input type="text" placeholder="Actual" class="form-control input-md" id="' + timestamp + 'row_input_actual" ></td><td><input type="text" placeholder="Paid" class="form-control input-md"  id="' + timestamp + 'row_input_paid"></td><td></td><td id="' + timestamp + 'row_save_action"><a class="btn-edit" onclick="insert_budget_list_row(' + timestamp + ',' + id + ')"><i class="fa fa-save"></i></a></td></tr>';
    jQuery('#' + id + '_add_row').append(add_row);
}
function insert_budget_list_row(itemid, category) {
    var item_name = jQuery("#" + itemid + "row_input_item").val();
    var item_estimate = jQuery("#" + itemid + "row_input_estimate").val();
    var item_actual = jQuery("#" + itemid + "row_input_actual").val();
    var item_paid = jQuery("#" + itemid + "row_input_paid").val();

    if (!jQuery.isNumeric(item_estimate)) {
        alert("Please enter Estimated value as Interger");
    }
    else if (!jQuery.isNumeric(item_actual)) {
        alert("Please enter Actual Paid value as Interger");
    }
    else if (!jQuery.isNumeric(item_paid)) {
        alert("Please enter Paid value as Interger");
    }
    else {
        action = 'addbudgetitem';
        $.post("/Dashboard/MyBudget",{
            'action': action,
            'item_name': item_name,
            'item_estimate': item_estimate,
            'item_actual': item_actual,
            'item_paid': item_paid,
            'item_category': category,
        }, function (data) {
            $("#" + itemid + "row_save_action").html('<strong>Saved</strong>');
        });
    }
}
function sub_budget_edit(itemid) {

    var budget_name_val = jQuery("#" + itemid + "_sub_add_row .budget_name").text();
    jQuery("#" + itemid + "_sub_add_row .budget_name").html('<input placeholder="Name" class="form-control input-md" id="sub_name_' + itemid + '" type="text" value="' + budget_name_val + '">')

    var budget_estimate_val = jQuery("#" + itemid + "_sub_add_row .budget_estimate").text();
    jQuery("#" + itemid + "_sub_add_row .budget_estimate").html('<input placeholder="Estimate" class="form-control input-md" id="sub_estimate_' + itemid + '" type="text" value="' + budget_estimate_val + '">')

    var budget_cost_val = jQuery("#" + itemid + "_sub_add_row .budget_cost").text();
    jQuery("#" + itemid + "_sub_add_row .budget_cost").html('<input placeholder="Cost" class="form-control input-md" id="sub_cost_' + itemid + '" type="text" value="' + budget_cost_val + '">')

    var budget_paid_val = jQuery("#" + itemid + "_sub_add_row .budget_paid").text();
    jQuery("#" + itemid + "_sub_add_row .budget_paid").html('<input placeholder="Paid" class="form-control input-md" id="sub_paid_' + itemid + '" type="text" value="' + budget_paid_val + '">')


    jQuery("#" + itemid + "_sub_add_row .action_perform").html('<a href="javascript:void();" onclick="sub_budget_list_edit(' + itemid + ')" class="btn-edit"><i class="fa fa-save"></i></a>')

}

function sub_budget_list_edit(itemid) {
    var item_name = jQuery("#sub_name_" + itemid).val();
    var item_estimate = jQuery("#sub_estimate_" + itemid).val();
    var item_actual = jQuery("#sub_cost_" + itemid).val();
    var item_paid = jQuery("#sub_paid_" + itemid).val();

    if (!jQuery.isNumeric(item_estimate)) {
        alert("Please enter Estimated value as Interger");
    }
    else if (!jQuery.isNumeric(item_actual)) {
        alert("Please enter Actual Paid value as Interger");
    }
    else if (!jQuery.isNumeric(item_paid)) {
        alert("Please enter Paid value as Interger");
    }
    else {
        action = 'editbudgetitem';
        $.post("/Dashboard/MyBudget", {
            'action': action,
            'item_name': item_name,
            'item_estimate': item_estimate,
            'item_actual': item_actual,
            'item_paid': item_paid,
            'itemid': itemid
        }, function (data) {
            jQuery('html, body').animate({ scrollTop: 0 }, 'slow');
            jQuery('#form-budget .status').show();
            jQuery('#form-budget .status').html("Saved");
            document.location.href = "/Dashboard/MyBudget";
        });
    }
}
function delete_budget_list(itemid) {
    var strconfirm = confirm("Are you sure you want to delete budget list?");
    if (strconfirm === true) {
        action = 'deletebudgetitem';
        $.post("/Dashboard/MyBudget", { action: action, itemid: itemid }, function (data) { document.location.href = "/Dashboard/MyBudget"});
    }
}

function delete_budget(categoryId) {
    var strconfirm = confirm("Are you sure you want to delete category?");
    if (strconfirm === true) {
        action = 'deletebudgetcategory';
        $.post("/Dashboard/MyBudget", { action: action, item_category: categoryId }, function (data) { document.location.href = "/Dashboard/MyBudget" });
    }
}
function delete_guestlist(itemid) {
    var strconfirm = confirm("Are you sure you want to delete this guest?");
    if (strconfirm === true) {
        action = 'deleteguest';
        $.post("/Dashboard/GuestList", { action: action, guestId: itemid }, function (data) {
            document.location.href = "/Dashboard/GuestList";
        });
    }
}
    

jQuery('.offeritem').on('click', function (e) {
    var itemid = this.attributes["data-id"].value;
    $.post("/cart/AddToBasket", { ItemId: itemid, Quantity: 1 }, function (msg) { alert("Item added to basket");})
});
jQuery('#offersinquiry').on('submit', (function (e) {
    jQuery('.status').show().html("Sending inquiry, please wait...");
    e.preventDefault();
    var formData = new FormData(this);
    jQuery.ajax({
        type: 'POST',
        url: $(this).attr('action'),
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            document.getElementById("book-now-on").disabled = false;
            $(".status").html("You inquiry was sent successfully.");
        },
        error: function (data) {
            console.log("error");
            console.log(data);
        }
    });

}));
jQuery('#commentform').on('submit', (function (e) {
    
    e.preventDefault();
    var formData = new FormData(this);
    jQuery.ajax({
        type: 'POST',
        url: $(this).attr('action'),
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            alert("Thank You!!\r\n We received your review, a moderator will check it before publishing it.");
        },
        error: function (data) {
            console.log("error");
            console.log(data);
        }
    });

}));