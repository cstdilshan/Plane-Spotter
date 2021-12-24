import React from "react";


export const columns = [
  {
    name: "Make",
    selector: row => row.make,
    //sortable: true
  },
  {
    name: "Model",
    selector: row => row.model,
    //sortable: true
  },
  {
    name: "Registration",
    selector: row => row.registration,
    //sortable: true,
  },
  {
    name: "Location",
    selector: row => row.location,
    //sortable: true
  }
];