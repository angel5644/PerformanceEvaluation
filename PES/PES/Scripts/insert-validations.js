$(document).ready(function () {
    defaultProfile();
});

var comboProfile = $('#selectedProfile');
var comboManager = $('#selectedManager');
var currentProfile;
var profile;
var manager;
var email;
var newProfile;
var curentManager;
var currentManagerProfile;

function defaultProfile() {

    currentManagerProfile = parseInt($("#currentProfile").val());
    comboProfile.children("option[value =" + (currentManagerProfile - 1) + "]").attr("selected", true);
    getValues();
}

function getValues() {
    profile = parseInt(comboProfile.val());
    manager = parseInt(comboManager.val());
    curentManager = parseInt($("#currentId").val());
    email = $('#Email').val();
    newProfile = 0;

    getProfile();
}

comboProfile.change(function () {
    getValues();
});

function getProfile() {
    if (profile == 1) {
        // Resource was selected
        newProfile = 2; // Manager
    } else if (profile == 2) {
        // Manager was selected
        newProfile = 3; // Director
    } else if (profile == 3) {
        // Director was selected
        newProfile = 3; // Director
    }
    getManagersByProfile(newProfile, email);
}

function getManagersByProfile(newProfile, email) {
    // Execute validate profile function
    $.getJSON('/Employee/GetEmployeesProifile?profile=' + newProfile + "&email=" + email, function (data) {
        if (profile == 1) {
            $('#profileLabel').text("New Manager");

            // Remove current options dropdown
            comboManager.children().remove();

            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                if (employee.EmployeeId == curentManager) {
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "' selected='selected'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
                else {
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
            }
            $('#dropdownManager').show();
        }
        else if (profile == 2) {
            $('#profileLabel').text("New Director");

            // Remove current options dropdown
            comboManager.children().remove();

            // Loop data from ajax call
            for (var i = 0; i < data.employees.length; i++) {
                var employee = data.employees[i];
                if (employee.EmployeeId == curentManager) {
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "' selected='selected'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
                else {
                    $('#selectedManager').append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
            }
            $('#dropdownManager').show();
        }
        else if (profile == 3) {
            // Remove current options dropdown
            comboManager.children().remove();
            $('#dropdownManager').hide();
        }
    });
}