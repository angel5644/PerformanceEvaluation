$(document).ready(function () {
    getNewManagersByProfile();
});

var currentProfile = parseInt($('#CurrentProfile_ProfileId').val());
var profile = parseInt($('#SelectedProfile').val());
var comboNewManager = $('#selectedNewManager');
var email = $('#Email').val();
var hasAssigned = $("#Assigned").val();

function getNewManagersByProfile() {
    // Execute validate profile function
        if (profile ==  || profile == 2 || profile == 3) {
            // Remove current options dropdown
            comboNewManager.children().remove();

            // Loop data from ajax call
            $.getJSON('/Employee/GetEmployeesProifile?profile=' + currentProfile + "&email=" + email, function (data) {
                for (var i = 0; i < data.employees.length; i++) {
                    var employee = data.employees[i];
                    comboNewManager.append("<option value='" + employee.EmployeeId + "'>" + employee.FirstName + " " + employee.LastName + "</option>");
                }
            });
            if (hasAssigned > 1) {

                $('#dropdownNewManager').show();
            }
            else {

                $('#dropdownNewManager').hide();
            }
        }
        //else if (profile == 3) {
        //    // Remove current options dropdown
        //    comboNewManager.children().remove();
        //    $('#dropdownNewManager').hide();
        //}
}