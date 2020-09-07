$(document).ready(function () {
   
    LoadPage();

    $("#btnExport").click(function () {
      
     GetEnrollmentHistoryFilters();
       
    });

   

});
function LoadPage() {
   
}

function GetEnrollmentHistoryFilters() {
    
    $.ajax({
        url: urlGetHistoryByFilters,
        dataType: 'json',
        beforeSend: function () {
            //start = (new Date()).getTime();
            //$.blockUI({ message: '<div class="icon-spinner9 icon-spin icon-lg"></div>', timeout: 60000, overlayCSS: { backgroundColor: "#000000", opacity: .8, cursor: "wait" }, css: { border: 0, padding: 0, backgroundColor: "transparent" } });
        },
        complete: function () {
            //end = (new Date()).getTime();
            //var total = end - start;
            //$.unblockUI();
        },
        data: BuildRequest(),
        type: 'POST',
        async: true,
        success: function (data) {
            console.log(data);
            let csv
            
            // Loop the array of objects
            for (let row = 0; row < data.records.length; row++) {
                let keysAmount = Object.keys(data.records[row]).length
                let keysCounter = 0

                // If this is the first row, generate the headings
                if (row === 0) {

                    // Loop each property of the object
                    for (let key in data.records[row]) {

                        // This is to not add a comma at the last cell
                        // The '\r\n' adds a new line
                        csv += key + (keysCounter + 1 < keysAmount ? ',' : '\r\n')
                        keysCounter++
                    }
                } else {
                    for (let key in data.records[row]) {
                        csv += data.records[row][key] + (keysCounter + 1 < keysAmount ? ',' : '\r\n')
                        keysCounter++
                    }
                }

                keysCounter = 0
            }

            // Once we are done looping, download the .csv by creating a link
            let link = document.createElement('a')
            link.id = 'download-csv'
            link.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(csv));
            link.setAttribute('download', 'Datos.csv');
            document.body.appendChild(link)
            document.querySelector('#download-csv').click()
            console.log(csv);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            swal({
                title: "Error!",
                text: textStatus,
                type: "error",
                confirmButtonText: "OK"
            });
        }
    });
}


function BuildRequest() {
    var fecini = null;
    var fecfin = null;
  

   
    fecini = $("#DateFrom").val();
    fecfin = $("#DateTo").val();

    
    var obj = new Object();
    obj.fecini = fecini;
    obj.fecfin = fecfin;
  

    return obj;
}



