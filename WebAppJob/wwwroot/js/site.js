﻿//let btnDeatil = document.querySelector("#btnDetail");

//btnDeatil.addEventListener("click", (e) => {

//    alert(e.type);

//    const httpRequest = new XMLHttpRequest();
//    httpRequest.onload = (response) => {

//        if (httpRequest.status == 200)
//            console.log(JSON.parse(response.target.response));

//        if (httpRequest.status == 500)
//            alert("Error");

//    }

//    const body = JSON.stringify({
//        Id: "48"
//    });

//    httpRequest.open("POST", "Home/GetInformationJob/",
//        false);
//    httpRequest.setRequestHeader("Content-Type", "application/json; charset=UTF-8")

//    httpRequest.send(body);

//});


//$('#tableApplications').DataTable({
//    ajax: {
//        url: location.origin + "/Home/Jobs",
//        type: 'GET'
//    },
//    processing: true,
//    serverSide : true
//});

//$('#tableApplications').DataTable();
//console.log(location.origin + "/Jobs")

function getDetailJob(obj) {
	$(".loader").show();
	let url = window.location.origin + '/Job/DetailJob/1';
	fetch(url)
		.then(response => response.text())
		.then(text => {
			document.querySelector("#job").innerHTML = text;
			$('#myModal').modal('show');
		});
	$(".loader").hide();
}

$('#tableApplications').DataTable({
	"ordering": true,
	"searching": true,
	pageLength: 10,
	"processing": true,
	deferRender: true,
	scrollY: 200,
	scrollCollapse: true,
	scroller: true,
	async: false,
	"serverSide": false,
	"filter": true,	
	"ajax": {
		"url": "/Home/GetApplicationsJobs",
		"type": "GET",
		dataSrc: "",
		"datatype": "json", error: function (jqXHR, ajaxOptions, thrownError) {
			//window.location.href = "/Citas/Error"
		}
	},

	columns: [

		{ 'data': 'company' },
		{ 'data': 'title' },
		{ 'data': 'status' },
		{ 'data': 'dateApply' },
		{
			mRender: function (data, type, row) {
				return '<a href="/Home/Eliminar/' + row.id + '">Eliminar</a> | <a data-bs-whatever="mdo" onclick="getDetailJob(this)" href="#'+row.id+'">Detalle</a> | <a data-bs-whatever="mdo" href="Home/Editar/' + row.id + '">Editar</a>'
			}
		}
	],
	dom: 'Bfrtip',
	"language": {
		"url": "//cdn.datatables.net/plug-ins/1.11.3/i18n/es_es.json",
	}
});
