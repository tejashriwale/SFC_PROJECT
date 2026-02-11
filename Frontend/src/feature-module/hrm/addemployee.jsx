import { ChevronUp, Info } from "feather-icons-react/build/IconComponents";
import {
  ArrowLeft,
  ChevronDown,
  LifeBuoy
} from "feather-icons-react/build/IconComponents";
import React, { useState, useEffect, useRef } from "react";
import { PlusCircle } from "react-feather";
import { Link } from "react-router-dom";
import Select from "react-select";
import { all_routes } from "../../Router/all_routes";
import { useDispatch, useSelector } from "react-redux";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { setToogleHeader } from "../../core/redux/action";
import Swal from "sweetalert2";
import withReactContent from "sweetalert2-react-content";
import { useNavigate } from "react-router-dom";
import { useLocation } from 'react-router-dom';
import axios from 'axios';
// import { ACSPLGUID } from "../../core/json/custom";
// import { ACSPLGUID, baseUrl, convertToISODate } from "../../core/json/custom";
import { ACSPLGUID, baseUrl } from "../../core/json/custom";
import { convertToISODate } from "../../core/json/custom";
import { getUserData } from "../../Context/UserData";
const AddEmployee = () => {

  const { userdetail } = getUserData();
  const route = all_routes;
  const dispatch = useDispatch();
  const data = useSelector((state) => state.toggle_header);

  const location = useLocation();
  const navigate = useNavigate();

  const generatedID = ACSPLGUID?.getNew();
  const { EMPID } = location.state || {};

  const GUID = ACSPLGUID.getNew()

  const renderCollapseTooltip = (props) => (
    <Tooltip id="refresh-tooltip" {...props}>
      Collapse
    </Tooltip>
  );
  console.log("DATA", EMPID);
  const isRestricted = userdetail.LANGUAGE === "en";
  // const [showPassword, setShowPassword] = useState(false);

  // const handleTogglePassword = () => {
  //   setShowPassword((prevShowPassword) => !prevShowPassword);
  // };
  // const [showConfirmPassword, setConfirmPassword] = useState(false);
  // const handleToggleConfirmPassword = () => {
  //   setConfirmPassword((prevShowPassword) => !prevShowPassword);
  // };

  const nameRef = useRef();
  const emailRef = useRef();
  const contactRef = useRef();
  const codeRef = useRef();
  const dobRef = useRef();
  const genderRef = useRef();
  const bloodGroupRef = useRef();
  const joinDateRef = useRef();
  const shiftRef = useRef();
  const departmentRef = useRef();
  const designationRef = useRef();
  const pincodeRef = useRef();
  const addressRef = useRef();
  const areaRef = useRef();
  const landmarkRef = useRef();
  const cityRef = useRef();
  const stateRef = useRef();
  const submitRef = useRef();

  const [formData, setFormData] = useState({
    EMPID: '',
    EMPNAME: '',
    EMPEMAIL: '',
    EMPCONTACT: '',
    EMPCODE: '',
    EMPDOB: '',
    EMPGENDER: '',
    EMPNATIONALITY: '',
    EMPJOINDATE: '',
    EMPSHIFT: '',
    EMPDEPARTMENT: '',
    EMPDESIGNATION: '',
    EMPBLOODGROUP: '',
    EMPPINCODE: '',
    EMPLANDMARK: '',
    EMPCITY: '',
    EMPSTATE: '',
    EMPADDRESS: '',
    EMPAREA: '',
    EMPPHOTO: '',
    EMPSTATUS: false,
    EMPDEPARTMENTid: '',
    EMPDESIGNATIONid: '',
    AADHAAR: ''
  });

  // const handleSelectChange = (selectedOption, fieldName) => {
  //   setFormData({
  //     ...formData,
  //     [fieldName]: selectedOption ? selectedOption.value : '',
  //   });
  // };

  // const handleChange = (e) => {
  //   const { name, value } = e.target;
  //   setFormData(prevData => ({
  //     ...prevData,
  //     [name]: value,
  //   }));
  // };
  const handleSubmit = (event) => {
    event.preventDefault();
    console.log('Form Data:', formData);
    showConfirmationAlert(event);
  };
  const handleFormSubmission = async () => {
    try {
      const payload1 = {
        "empid": EMPID ? EMPID : GUID,
        "empname": formData.EMPNAME,
        "empemail": formData.EMPEMAIL,
        "empcontact": formData.EMPCONTACT,
        "empcode": formData.EMPCODE,
        "empdob": formData.EMPDOB,
        "empgender": formData.EMPGENDER,
        "empnationality": "",
        "empjoindate": formData.EMPJOINDATE,
        "empshift": formData.EMPSHIFT,
        "empdepartment": formData.EMPDEPARTMENT,
        "empdesignation": formData.EMPDESIGNATION,
        "empbloodgroup": formData.EMPBLOODGROUP,
        "emppincode": formData.EMPPINCODE,
        "emplandmark": formData.EMPLANDMARK,
        "empcity": formData.EMPCITY,
        "empstate": formData.EMPSTATE,
        "empaddress": formData.EMPADDRESS,
        "emparea": formData.EMPAREA,
        "empphoto": formData.EMPPHOTO,
        "empstatus": formData.EMPSTATUS ? formData.EMPSTATUS.toString() : "false",
        "companyid": userdetail?.companyID || "",
        "deptid": userdetail?.departmentID || "",
        "aadhaar": formData?.AADHAAR || ""
      };

      const headers = {
        "Content-Type": "application/json",
        Accept: "*/*",
      };

      const response = await axios({
        method: "POST",
        url: baseUrl.Url + "/backend/api/SP_AddUpdHRMEmployees",
        data: JSON.stringify(payload1),
        headers: headers,
      });

      console.log("payload1", payload1);


      if (response.status === 200) {
        Swal.fire({
          icon: "success",
          title: "Saved!",
          text: "Data saved successfully.",
          confirmButtonText: "OK",
        });


        setFormData({
          EMPNAME: '',
          EMPEMAIL: '',
          EMPCONTACT: '',
          EMPCODE: '',
          EMPDOB: '',
          EMPGENDER: '',
          EMPJOINDATE: '',
          EMPSHIFT: '',
          EMPDEPARTMENT: '',
          EMPDESIGNATION: '',
          EMPBLOODGROUP: '',
          EMPPINCODE: '',
          EMPLANDMARK: '',
          EMPCITY: '',
          EMPSTATE: '',
          EMPADDRESS: '',
          EMPAREA: '',
          EMPPHOTO: '',
          EMPSTATUS: false,
        });


        navigate(route.EmployeeMaster)

      } else {

        Swal.fire({
          icon: "error",
          title: "Error",
          text: "Failed to save data. Please try again.",
        });
      }
    } catch (error) {
      console.error("Submission Error:", error);
      Swal.fire({
        icon: "error",
        title: "Error",
        text: "Failed to save data. Please try again.",
      });
    }
  };

  const handleEnterKey = (e, nextRef) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      if (nextRef && nextRef.current) {
        nextRef.current.focus();
      }
    }
  };

  const MySwal = withReactContent(Swal);

  const showConfirmationAlert = (event) => {
    MySwal.fire({
      title: "Are you sure?",
      text: "Do you want to save this data?",
      showCancelButton: true,
      confirmButtonColor: "#00ff00",
      confirmButtonText: "SAVE",
      cancelButtonColor: "#092C4C",
      cancelButtonText: "CANCLE",
    }).then((result) => {
      if (result.isConfirmed) {
        handleFormSubmission(event);
      }
    });
  };
  const showExitAlert = () => {
    MySwal.fire({
      title: "Are you sure?",
      text: "Do you want to Exit?",
      showCancelButton: true,
      confirmButtonColor: "#00ff00",
      confirmButtonText: "YES",
      cancelButtonColor: "#092C4C",
      cancelButtonText: "NO",
    }).then((result) => {
      if (result.isConfirmed) {
        navigate(route.EmployeeMaster)
      }
    });
  };


  const [gender, setgender] = useState([]);
  const [bloodgroup, setbloodgroup] = useState([]);

  const fetchgenders = async () => {
    try {
      const response = await axios.get(
        baseUrl.Url + "/backend/api/Implications/GNID",

      );

      if (response.status !== 200) throw new Error("Failed to fetch implications data");

      const data = response.data;
      const implicationsDropdown = data.map(({ iTitle, iValue }) => ({
        label: iTitle,
        value: iValue,
      }));

      setgender(implicationsDropdown);
    } catch (error) {
      console.error("Error fetching implications:", error);
    }
  };


  const fetchbloodgroups = async () => {
    try {
      const response = await axios.get(
        baseUrl.Url + "/backend/api/Implications/BLOODG",

      );

      if (response.status !== 200) throw new Error("Failed to fetch implications data");

      const data = response.data;
      const implicationDropdown = data.map(({ iTitle, iValue }) => ({
        label: iTitle,
        value: iValue,
      }));

      setbloodgroup(implicationDropdown);
    } catch (error) {
      console.error("Error fetching implications:", error);
    }
  };


  useEffect(() => {
    fetchgenders();
    fetchbloodgroups();
  }, []);


  const [sellingtype, setsellingtype] = useState([]);
  const [statetype, setstatetype] = useState([]);

  useEffect(() => {
    const fetchLocationData = async () => {
      try {
        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        const payload = { spincode: "%" };


        const response = await axios({
          method: "POST",
          url: baseUrl.Url + "/backend/api/StatePincode",
          data: JSON.stringify(payload),
          headers: headers,
        });

        if (response.status === 200) {
          const data = response.data;
          const districtdata = data
            .map(({ sdistrict, said }) => ({
              label: sdistrict,
              value: said,
            }));
          setsellingtype(districtdata)
          const Statedata = data
            .map(({
              sstatename, said }) => ({
                label: sstatename,
                value: said,
              }));
          setstatetype(Statedata);
        } else {
          console.error("Failed to fetch district and state for the pincode");
        }
      } catch (error) {
        console.error("Error fetching district and state data:", error);
      }
    }
    fetchLocationData();
  }, []);



  // const handleChange = async (e) => {
  //   const { name, value } = e.target;
  //   setFormData((prevData) => ({
  //     ...prevData,
  //     [name]: value,
  //   }));

  //   if (name === "EMPPINCODE" && value.length === 6) {
  //     try {
  //       const headers = {
  //         "Content-Type": "application/json",
  //         Accept: "*/*",
  //       };

  //       const payload = { spincode: value };


  //       const response = await axios({
  //         method: "POST",
  //         url: baseUrl.Url + "/backend/api/StatePincode",
  //         data: JSON.stringify(payload),
  //         headers: headers,
  //       });

  //       if (response.status === 200) {
  //         const data = response.data;
  //         const districtdata = data
  //           .map(({ sdistrict, said }) => ({
  //             label: sdistrict,
  //             value: said,
  //           }));
  //         setsellingtype(districtdata)
  //         const Statedata = data
  //           .map(({ sstatename, said }) => ({
  //             label: sstatename,
  //             value: said,
  //           }));
  //         setstatetype(Statedata)
  //         setFormData((prevData) => ({
  //           ...prevData,
  //           EMPSTATE: response.data[0].said || '',
  //           EMPCITY: response.data[0].said || '',
  //         }));
  //       } else {
  //         console.error("Failed to fetch district and state for the pincode");
  //       }
  //     } catch (error) {
  //       console.error("Error fetching district and state data:", error);
  //     }
  //   }
  // };

  const handleChange = async (e) => {
    const { name, value } = e.target;

    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));

    if (name === "EMPPINCODE") {
      // Validate pincode (only proceed if it's exactly 6 digits)
      if (/^\d{6}$/.test(value)) {
        try {
          const headers = {
            "Content-Type": "application/json",
            Accept: "*/*",
          };
          const payload = { spincode: value };
          const response = await axios.post(
            baseUrl.Url + "/backend/api/StatePincode",
            JSON.stringify(payload),
            { headers }
          );

          if (response.status === 200) {
            const data = response.data;

            const districtdata = data.map(({ sdistrict, said }) => ({
              label: sdistrict,
              value: said,
            }));
            setsellingtype(districtdata);

            const Statedata = data.map(({ sstatename, said }) => ({
              label: sstatename,
              value: said,
            }));
            setstatetype(Statedata);

            setFormData((prevData) => ({
              ...prevData,
              EMPSTATE: response.data[0]?.said || '',
              EMPCITY: response.data[0]?.said || '',
            }));
          } else {
            console.error("Failed to fetch district and state for the pincode");
          }
        } catch (error) {
          console.error("Error fetching district and state data:", error);
        }
      } else {
        console.log("Invalid pincode entered. It must be 6 digits.");
      }
    }
  };
  const [designations, setDesignations] = useState([]);

  useEffect(() => {
    const fetchDesignations = async () => {
      try {
        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        const payload = {
          companyid: userdetail?.companyID ? userdetail.companyID : "",
          deptid: userdetail?.departmentID ? userdetail.departmentID : ""
        };

        const response = await axios.post(
          `${baseUrl.Url}/backend/api/GET_HRMDesignationName`,
          payload,
          { headers }
        );
        if (response.status !== 200)
          throw new Error("Failed to fetch vendor data");
        console.log("requisition setails", response.data)
        const data = response.data;
        const designationsData = data
          .map(({ dname, daid }) => ({
            label: dname,
            value: daid,
          }));
        setDesignations(designationsData);
      } catch (error) {
        console.error("Error fetching vendor data:", error);
      }
    };

    fetchDesignations();
  }, []);


  const [shifts, setShifts] = useState([]);

  useEffect(() => {
    const fetchShifts = async () => {
      try {
        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        const payload = {
          companyid: userdetail?.companyID ? userdetail.companyID : "",
          deptid: userdetail?.departmentID ? userdetail.departmentID : ""
        };

        const response = await axios.post(
          `${baseUrl.Url}/backend/api/GET_HRMShiftInfoName`,
          payload,
          { headers }
        );
        if (response.status !== 200)
          throw new Error("Failed to fetch vendor data");
        console.log("requisition setails", response.data)
        const data = response.data;
        const shiftsData = data
          .map(({ shname, shid }) => ({
            label: shname,
            value: shid,
          }));
        setShifts(shiftsData);
      } catch (error) {
        console.error("Error fetching vendor data:", error);
      }
    };

    fetchShifts();
  }, []);



  const [departments, setDepartments] = useState([]);

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const headers = {
          "Content-Type": "application/json",
          Accept: "*/*",
        };

        const payload = {
          companyid: userdetail?.companyID
        };

        const response = await axios.post(
          `${baseUrl.Url}/backend/api/GET_DepartmentName`,
          payload,
          { headers }
        );
        if (response.status !== 200)
          throw new Error("Failed to fetch vendor data");
        console.log("requisition setails", response.data)
        const data = response.data;
        const departmentsData = data
          .map(({ dname, deptaid }) => ({
            label: dname,
            value: deptaid,
          }));
        setDepartments(departmentsData);
      } catch (error) {
        console.error("Error fetching vendor data:", error);
      }
    };

    fetchDepartments();
  }, []);




  const [selectedFile, setSelectedFile] = useState(null);

  // // Handle file selection
  // const handleFileChange = (event) => {
  //   const file = event.target.files[0]; // Get the first selected file
  //   if (file) {
  //     setSelectedFile(file);
  //   }
  // };


  // const handleFileChange = (event) => {
  //   const file = event.target.files[0]; // Get the first selected file
  //   if (file) {
  //     setSelectedFile(file);
  //     setFormData({ ...formData, EMPPHOTO: file.name }); // Update formData with the selected file
  //   }
  // };


  useEffect(() => {
    const fetchData = async () => {
      if (EMPID) {
        try {
          const payload = {
            "empid": EMPID,
            "keyword": "%",
            "companyid": userdetail?.companyID ? userdetail.companyID : "",
            "deptid": userdetail?.departmentID ? userdetail.departmentID : ""
          };

          const headers = {
            "Content-Type": "application/json",
            Accept: "*/*",
          };

          // Make the API request with async/await
          const response = await axios({
            method: "POST",
            url: baseUrl.Url + "/backend/api/GET_HRMEmployees",
            data: JSON.stringify(payload),
            headers: headers,
          });

          if (response.status !== 200) {
            throw new Error("Failed to Fetch Data");
          }

          let apiData = response.data[0];

          console.log("API Data:", apiData);
          setFormData((prev) => ({
            ...prev,
            EMPID: apiData.empid,
            EMPNAME: apiData.empname,
            EMPEMAIL: apiData.empemail,
            EMPCONTACT: apiData.empcontact,
            EMPCODE: apiData.empcode,
            EMPDOB: convertToISODate(apiData.empdob),
            EMPGENDER: apiData.empgender,
            EMPNATIONALITY: "",
            EMPJOINDATE: convertToISODate(apiData.empjoindate),
            EMPSHIFT: apiData.empshift,
            EMPDEPARTMENTid: apiData.dname,
            EMPDESIGNATIONid: apiData.dsname,
            EMPDESIGNATION: apiData.empdesignation,
            EMPDEPARTMENT: apiData.empdepartment,
            EMPBLOODGROUP: apiData.empbloodgroup,
            EMPPINCODE: apiData.emppincode,
            EMPLANDMARK: apiData.emplandmark,
            EMPCITY: apiData.empcity,
            EMPSTATE: apiData.empstate,
            // EMPCITY: sellingtype.find((city) => city.value == apiData.empcity)?.label || "",
            // EMPSTATE: statetype.find((state) => state.value == apiData.empstate)?.value || "",
            EMPADDRESS: apiData.empaddress,
            EMPAREA: apiData.emparea,
            EMPPHOTO: apiData.empphoto,
            EMPSTATUS: typeof apiData.empstatus === 'string' ? apiData.empstatus === 'true' : Boolean(apiData.empstatus),
            AADHAAR: apiData.aadhaar,
          }));
        } catch (error) {
          console.error("Error fetching Access Right Data:", error);
        }
      }
    };

    fetchData();
  }, [EMPID]);


  // const onStatusChange = (event) => {
  //   const { checked } = event.target;
  //   setFormData({
  //     ...formData,
  //     EMPSTATUS: checked ? 1 : 0,
  //   });
  // };

  const handleStatusChange = () => {
    setFormData({ ...formData, EMPSTATUS: !formData.EMPSTATUS });
  };

  // const uploadProfile = async (fileData) => {
  //   const formData = new FormData();
  //   formData.append("Files", fileData);
  //   formData.append("FileNames", fileData.name);
  //   formData.append("FileDescription", "Profile Image");

  //   try {
  //     const response = await axios.post(
  //       `${baseUrl.Url}/backend/api/ProfileUpload/Upload`,
  //       formData,
  //       {
  //         headers: {
  //           "Content-Type": "multipart/form-data",
  //         },
  //       }
  //     );
  //     console.log("✅ Upload Success:", response.data);
  //     alert("फोटो यशस्वीरित्या अपलोड झाला!");
  //   } catch (error) {
  //     console.error("❌ Upload Error:", error.message);
  //     alert("फोटो अपलोड होताना त्रुटी आली.");
  //   }
  // };


  // const uploadProfile = async (fileData) => {
  //   // Generate unique ID
  //   let generatedID = ACSPLGUID?.getNew();
  //   if (!generatedID) {
  //     console.error("Error: Failed to generate unique ID.");
  //     return;
  //   }
  //   console.log("Generated ID:", generatedID);

  //   // Prepare final file name with unique ID
  //   let finalFileName = fileData.name;
  //   finalFileName = `${generatedID}_${finalFileName}`;
  //   console.log("Final File Name:", finalFileName);

  //   // Create form data for file upload
  //   const formData = new FormData();
  //   formData.append("Files", fileData);
  //   formData.append("FileNames", finalFileName);
  //   formData.append("FileSizeInBytes", fileData.size);
  //   formData.append("FilePath", `/Images/${finalFileName}`);
  //   formData.append("FileDescription", "Profile Image");

  //   try {
  //     const response = await axios.post(
  //       `${baseUrl.Url}/backend/api/ProfileUpload/Upload`,
  //       formData,
  //       {
  //         headers: {
  //           "Content-Type": "multipart/form-data",
  //         },
  //       }
  //     );
  //     console.log("✅ Upload Success:", response.data);
  //     alert("फोटो यशस्वीरित्या अपलोड झाला!");
  //   } catch (error) {
  //     console.error("❌ Upload Error:", error.message);
  //     alert("फोटो अपलोड होताना त्रुटी आली.");
  //   }
  // };



  // const handleFileChange = (e) => {
  //   const file = e.target.files[0];
  //   if (file) {
  //     setSelectedFile(file);
  //     setFormData({ ...formData, EMPPHOTO: file.name });
  //     uploadProfile(file);
  //   }
  // };

  const uploadProfile = async (fileData, uniqueFileName) => {
    const formData = new FormData();
    formData.append("Files", fileData);
    formData.append("FileNames", uniqueFileName);
    formData.append("FileSizeInBytes", fileData.size);
    formData.append("FilePath", `/Images/${uniqueFileName}`);
    formData.append("FileDescription", "Profile Image");


    const messageContainer = document.getElementById("messageContainer");

    try {
      const response = await axios.post(
        `${baseUrl.Url}/backend/api/ProfileUpload/Upload`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );
      console.log("✅ Upload Success:", response.data);
      // alert("फोटो यशस्वीरित्या अपलोड झाला!");
      messageContainer.innerHTML = isRestricted ? "Validation Error" : "फोटो यशस्वीरित्या अपलोड झाला!";
      messageContainer.style.color = "green";
    } catch (error) {
      console.error("❌ Upload Error:", error.message);
      // alert("फोटो अपलोड होताना त्रुटी आली.");
      messageContainer.innerHTML = isRestricted
        ? "Validation Error"
        : "फोटो अपलोड होताना त्रुटी आली.";

      messageContainer.style.color = "red";
    }
  };

  // Handle the file selection and set the form data
  const handleFileChange = (e) => {
    const file = e.target.files[0];
    if (file) {

      if (!generatedID) {
        console.error("Error: Failed to generate unique ID.");
        return;
      }
      console.log("Generated ID:", generatedID);

      // Prepare final file name with unique ID
      let finalFileName = `${generatedID}_${file.name}`;
      console.log("Final File Name:", finalFileName);

      // Set selected file and form data (store unique file name)
      setSelectedFile(file);
      setFormData({ ...formData, EMPPHOTO: finalFileName });

      // Call the upload function with the file and updated unique file name
      uploadProfile(file, finalFileName);
    }
  };


  const validateinput = (e) => {


    // Common pattern for names (Marathi + English letters, digits, spaces) — 3 to 50 chars, no leading/trailing spaces
    const nameRegex = /^(?!\s)([A-Za-z0-9\u0900-\u097F\s]{3,50})(?<!\s)$/;

    const { EMPNAME } = formData;

    if (!EMPNAME || !nameRegex.test(EMPNAME)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Name must be valid. 3 to 50 characters. No spaces at start or end. Marathi, English letters, digits, and spaces allowed."
          : "नाव वैध असावे. 3 ते 50 अक्षरे. सुरुवातीला किंवा शेवटी रिकामी जागा नसावी. मराठी, इंग्रजी अक्षरे, अंक, व स्पेस चालतील."
      }).then(() => {
        nameRef.current.focus();
      });
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;

    const { EMPEMAIL } = formData;

    if (!EMPEMAIL || !emailRegex.test(EMPEMAIL)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Email must be valid. Example: example@domain.com"
          : "ईमेल वैध असावा. उदाहरण: example@domain.com"

      }).then(() => {
        emailRef.current.focus();
      });
      return;
    }

    const contactRegex = /^([0-9\u0966-\u096F]{10})$/;

    const { EMPCONTACT } = formData;

    if (!EMPCONTACT || !contactRegex.test(EMPCONTACT)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Contact number must be 10 digits (0-9 only)."
          : "संपर्क क्रमांक 10 अंकी असावा (०-९ किंवा 0-9)."
      }).then(() => {
        contactRef.current.focus();
      });
      return;
    }

    const empCodeRegex = /^[A-Za-z0-9]{3,15}$/;
    const { EMPCODE } = formData;

    if (!EMPCODE || !empCodeRegex.test(EMPCODE)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Employee code must be valid (3 to 15 English letters/numbers, no special characters or spaces)."
          : "कर्मचारी कोड वैध असावा (3 ते 15 इंग्रजी अक्षरे/अंक, विशेष चिन्हे व स्पेस नसावेत).",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        codeRef.current.focus();
      });
      return;
    }

    const { EMPDOB } = formData;

    if (!EMPDOB) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a valid Date of Birth."
          : "जन्मतारीख निवडली जावी.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        dobRef.current.focus();
      });
      return;
    }

    const { EMPGENDER } = formData;

    if (!EMPGENDER) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a gender."
          : "लिंग निवडले जावे.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        genderRef.current.focus();
      });
      return;
    }

    const { EMPBLOODGROUP } = formData;

    if (!EMPBLOODGROUP) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a blood group."
          : "रक्तगट निवडला जावा.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        bloodGroupRef.current.focus();
      });
      return;
    }

    const { EMPJOINDATE } = formData;

    if (!EMPJOINDATE) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a joining date."
          : "जॉइनिंग तारीख निवडली जावी.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        joinDateRef.current.focus();
      });
      return;
    }

    const { EMPSHIFT } = formData;

    if (!EMPSHIFT) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a shift."
          : "शिफ्ट निवडली जावी.",
        allowOutsideClick: false
      }).then(() => {
        shiftRef.current.focus();
      });
      return;
    }

    const { EMPDEPARTMENT } = formData;

    if (!EMPDEPARTMENT) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a department."
          : "विभाग निवडला जावा.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        departmentRef.current.focus();
      });
      return;
    }

    const { EMPDESIGNATION } = formData;

    if (!EMPDESIGNATION) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a post."
          : "पद निवडले जावे.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        designationRef.current.focus();
      });
      return;
    }

    const pincodeRegex = /^[0-9\u0966-\u096F]{6}$/;
    const { EMPPINCODE } = formData;

    if (!EMPPINCODE || !pincodeRegex.test(EMPPINCODE)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Pincode must be 6 digits (0-9 only)."
          : "पिनकोड 6 अंकी असावा (०-९ किंवा 0-9).",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        pincodeRef.current.focus();
      });
      return;
    }

    const areaRegex = /^[A-Za-z0-9\u0900-\u097F\s,.'-]{3,100}$/;
    const { EMPAREA } = formData;

    if (!EMPAREA || !areaRegex.test(EMPAREA)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Area, sector, or village must be 3 to 100 characters (letters, numbers, spaces, and common symbols are allowed)."
          : "क्षेत्र, सेक्टर, गाव 3 ते 100 अक्षरे असावे (अक्षरे, अंक, जागा व सहजरित्या वापरलेली विशेष चिन्हे मंजूर आहेत).",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        areaRef.current.focus();
      });
      return;
    }

    const landmarkRegex = /^[A-Za-z0-9\u0900-\u097F\s,.'-]{3,100}$/;
    const { EMPLANDMARK } = formData;

    // Only validate if Landmark is provided
    if (EMPLANDMARK && !landmarkRegex.test(EMPLANDMARK)) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Landmark must be 3 to 100 characters (letters, numbers, spaces, and common symbols are allowed)."
          : "लँडमार्क 3 ते 100 अक्षरे असावे (अक्षरे, अंक, जागा व सहजरित्या वापरलेली विशेष चिन्हे मंजूर आहेत).",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        landmarkRef.current.focus();
      });
      return;
    }

    const { EMPCITY, EMPSTATE } = formData;

    // Validate City
    if (!EMPCITY) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a city."
          : "शहर निवडले जावे.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        cityRef.current.focus();
      });
      return;
    }

    // Validate State
    if (!EMPSTATE) {
      Swal.fire({
        icon: "error",
        title: isRestricted ? "Validation Error" : "वैधता त्रुटी",
        text: isRestricted
          ? "Please select a state."
          : "राज्य निवडले जावे.",
        allowOutsideClick: false,
        allowEscapeKey: false
      }).then(() => {
        stateRef.current.focus();
      });
      return;
    }


    // ✅ All validations passed
    handleSubmit(e);
  };

  useEffect(() => {
    const handleShortcut = (e) => {

      if (e.ctrlKey && e.key === 'e' || e.ctrlKey && e.key === 'E') {
        e.preventDefault();
        showExitAlert();
      }
      if (e.ctrlKey && e.key === 's' || e.ctrlKey && e.key === 'S') {
        e.preventDefault();
        validateinput(e);
      }

    };

    window.addEventListener('keydown', handleShortcut);

    return () => {
      window.removeEventListener('keydown', handleShortcut);
    };
  }, [navigate, formData]);




  return (
    <div>
      <div className="page-wrapper ">
        <div className="content">
          <div className="page-header">
            <div className="add-item d-flex">
              <div className="page-title">
                <h4> {isRestricted ? "Employee" : "नवीन कर्मचारी"} </h4>
                <h6>{isRestricted ? "Add New Employee" : "नवीन कर्मचारी तयार करा"}  </h6>

              </div>
            </div>
            <ul className="table-top-head">
              <li>
                <div className="page-btn">
                  <Link to={route.EmployeeMaster} className="btn btn-secondary">
                    <ArrowLeft className="me-2" />
                    {isRestricted ? "back" : "मागे"}
                  </Link>
                </div>
              </li>
              <li>
                <OverlayTrigger placement="top" overlay={renderCollapseTooltip}>
                  <Link
                    data-bs-toggle="tooltip"
                    data-bs-placement="top"
                    id="collapse-header"
                    className={data ? "active" : ""}
                    onClick={() => {
                      dispatch(setToogleHeader(!data));
                    }}
                  >
                    <ChevronUp />
                  </Link>
                </OverlayTrigger>
              </li>
            </ul>
          </div>
          {/* /product list */}
          <form onSubmit={handleSubmit}>
            <div className="card mbgcolor">
              <div className="card-body mbgcolor">
                <div className="new-employee-field">
                  <div className="card-title-head">
                    <h6>
                      <span>
                        <Info className="feather-edit" />
                      </span>
                      {isRestricted ? "Employee Details" : " कर्मचारी माहिती"}

                    </h6>
                  </div>
                  {/* <div className="profile-pic-upload">
                    <div className="profile-pic">
                      <span>
                        <PlusCircle className="plus-down-add" />
                        Profile Photo
                      </span>
                      <div>
                        {renderImagePreview()}
                      </div>
                    </div>
                    <div className="input-blocks mb-0">
                      <div className="image-upload mb-0">
                        <input
                          type="file"
                          onChange={handleFileChange} // Handle file change
                          accept="image/*"             // Accepts only image files
                          multiple={false}             // Ensures only one file can be selected
                        />
                        <div className="image-uploads">
                          <h4>Change Image</h4>
                        </div>
                      </div>
                    </div>
                  </div> */}
                  <div className="profile-pic-upload">
                    <div className="profile-pic">
                      {!selectedFile && (
                        <span>
                          <PlusCircle className="plus-down-add" />
                          {isRestricted ? "Profile Image" : "प्रोफाईल फोटो"}
                        </span>
                      )}
                      <div>
                        {/* Display the image preview */}
                        {selectedFile && (
                          <img
                            src={URL.createObjectURL(selectedFile)}
                            alt="Profile Preview"
                            width="100"
                            height="100"
                            style={{ borderRadius: '5%' }}
                          />
                        )}
                      </div>
                    </div>

                    <div className="input-blocks mb-0">
                      <div className="image-upload mb-0">
                        <input
                          type="file"
                          onChange={handleFileChange}
                          // onChange={uploadProfile}
                          accept="image/*"
                          multiple={false}
                        />
                        <div className="image-uploads">
                          <h4>{isRestricted ? "Photo Badala" : "फोटो बदला"} </h4>
                        </div>
                      </div>
                    </div>
                    <div className="mb-0">
                      <div className="status-toggle modal-status d-flex justify-content-between">
                        <span className="status-label">{isRestricted ? "status" : "स्थिती"}  </span>
                        <div className="d-flex align-items-center">
                          <input
                            type="checkbox"
                            id="user2"
                            className="check"
                            name="EMPSTATUS"
                            checked={formData.EMPSTATUS}
                            onChange={handleStatusChange}
                          />
                          <label htmlFor="user2" className="checktoggle" />
                        </div>
                      </div>
                    </div>


                  </div>
                  <div className="row">
                    <div className="col-lg-4 col-md-6">
                      <div className="mb-3">
                        <label className="form-label required">{isRestricted ? "Name" : "नाव"} </label>
                        <input type="text"
                          className="form-control"
                          name="EMPNAME"
                          value={formData.EMPNAME}
                          onChange={handleChange}
                          onKeyDown={(e) => handleEnterKey(e, emailRef)}
                          ref={nameRef}
                          required
                          autoFocus
                        />
                      </div>
                    </div>

                    <div className="col-lg-4 col-md-6">
                      <div className="mb-3">
                        <label className="form-label required">{isRestricted ? "E-Mail" : "ईमेल"} </label>
                        <input type="email"
                          className="form-control"
                          name="EMPEMAIL"
                          value={formData.EMPEMAIL}
                          onChange={handleChange}
                          onKeyDown={(e) => handleEnterKey(e, contactRef)}
                          ref={emailRef}
                          required
                        />
                      </div>
                    </div>
                    <div className="col-lg-4 col-md-6 col-6">
                      <div className="mb-3">
                        <label className="form-label required">{isRestricted ? "mobile no." : "मोबाईल नं."}</label>
                        <input type="text"
                          className="form-control"
                          name="EMPCONTACT"
                          maxLength={10}
                          value={formData.EMPCONTACT}
                          onChange={handleChange}
                          onKeyDown={(e) => handleEnterKey(e, codeRef)}
                          ref={contactRef}
                          required
                        />
                      </div>
                    </div>

                    <div className="col-lg-3 col-md-6  col-6">
                      <div className="mb-3">
                        <label className="form-label">{isRestricted ? "Employee Code" : "कर्मचारी कोड"} </label>
                        <input
                          type="text"
                          className="form-control"
                          name="EMPCODE"
                          value={formData.EMPCODE}
                          onKeyDown={(e) => handleEnterKey(e, dobRef)}
                          ref={codeRef}
                          onChange={handleChange}
                        />
                      </div>
                    </div>

                    <div className="col-lg-3 col-md-6 col-6">
                      <div className="mb-3">
                        <label className="form-label required">{isRestricted ? "BirthDate" : "जन्मतारीख"} </label>
                        <div className="input-groupicon calender-input">
                          <input
                            type="date"
                            className="form-control"
                            name="EMPDOB"
                            value={formData.EMPDOB}
                            onKeyDown={(e) => handleEnterKey(e, genderRef)}
                            ref={dobRef}
                            onChange={(e) => {
                              setFormData((prevData) => ({
                                ...prevData,
                                EMPDOB: e.target.value,
                              }));
                            }}
                            placeholder="Choose Date"
                            max={new Date().toISOString().split("T")[0]}
                            required
                          />
                        </div>
                      </div>
                    </div>

                    <div className="col-lg-3 col-md-6 col-6">
                      <div className="mb-3">
                        <label className="form-label">{isRestricted ? "Gender" : "लिंग"} </label>
                        <Select
                          classNamePrefix="react-select"
                          name="EMPGENDER"
                          options={gender}
                          value={gender.find((option) => option.value === formData.EMPGENDER) || null}
                          placeholder={`${isRestricted ? "select" : "निवडा"}`}
                          // onChange={handleSelectChange}
                          onChange={(selectedOption) => {
                            setFormData((prevData) => ({
                              ...prevData,
                              EMPGENDER: selectedOption ? selectedOption.value : "",
                            }));
                            if (bloodGroupRef.current) {
                              bloodGroupRef.current.focus();
                            }
                          }}
                          // onKeyDown={(e) => handleEnterKey(e, bloodGroupRef)}
                          ref={genderRef}
                        // required
                        />
                      </div>
                    </div>

                    <div className="col-lg-3 col-md-6 col-12">
                      <div className="mb-3">
                        <label className="form-label">{isRestricted ? "Blood-Group" : "रक्तगट"} </label>
                        <Select
                          classNamePrefix="react-select"
                          name="EMPBLOODGROUP"
                          options={bloodgroup}
                          placeholder={`${isRestricted ? "Select" : "निवडा"}`}
                          value={bloodgroup.find((option) => option.value === formData.EMPBLOODGROUP) || null}
                          // onChange={handleSelectChange}
                          onChange={(selectedOption) => {
                            setFormData((prevData) => ({
                              ...prevData,
                              EMPBLOODGROUP: selectedOption ? selectedOption.value : "",
                            }));
                            if (joinDateRef.current) {
                              joinDateRef.current.focus();
                            }
                          }}
                          // onKeyDown={(e) => handleEnterKey(e, joinDateRef)}
                          ref={bloodGroupRef}
                        // required
                        />
                      </div>
                    </div>

                    <div className="row">
                      <div className="col-lg-3 col-md-6 col-6">

                        <label className="form-label required">{isRestricted ? "Registration Date" : "सदस्यता दिनांक"} </label>
                        <div className="input-groupicon calender-input">
                          <input
                            type="date"
                            className="form-control"
                            name="EMPJOINDATE"
                            value={formData.EMPJOINDATE}
                            onChange={(e) => {
                              setFormData((prevData) => ({
                                ...prevData,
                                EMPJOINDATE: e.target.value,
                              }));
                              if (shiftRef.current) {
                                shiftRef.current.focus();
                              }
                            }}
                            // onKeyDown={(e) => handleEnterKey(e, shiftRef)}
                            ref={joinDateRef}
                            max={new Date().toISOString().split("T")[0]}
                            required
                          />
                        </div>

                      </div>

                      <div className="col-lg-3 col-md-6 col-6">
                        <div className="mb-3">
                          <label className="form-label">{isRestricted ? "Designation" : "पदनाम"} </label>
                          <Select
                            classNamePrefix="react-select"
                            name="EMPDESIGNATION"
                            options={designations}
                            value={designations.find((option) => option.value === formData.EMPDESIGNATION) || null}
                            // onChange={(selectedOption) =>
                            //   handleSelectChange(selectedOption, 'EMPDESIGNATION')
                            // }
                            onChange={(selectedOption) => {
                              setFormData((prevData) => ({
                                ...prevData,
                                EMPDESIGNATION: selectedOption ? selectedOption.value : "",
                              }));
                              if (pincodeRef.current) {
                                pincodeRef.current.focus();
                              }
                            }}
                            // onKeyDown={(e) => handleEnterKey(e, pincodeRef)}
                            ref={designationRef}
                            placeholder={`${isRestricted ? "Select" : "निवडा"}`}
                          />
                        </div>
                      </div>

                      <div className="col-lg-3 col-md-6 col-12">
                        <div className="mb-3">
                          <div className="add-newplus">
                            <label className="form-label">{isRestricted ? "Shift" : "शिफ्ट"} </label>
                            {/* <Link to="#">
                              <span>
                                <PlusCircle className="plus-down-add" />
                                {isRestricted ? "New Add" : "नवीन जोडा"}
                              </span>
                            </Link> */}
                          </div>

                          <Select
                            classNamePrefix="react-select"
                            name="EMPSHIFT"
                            options={shifts}
                            value={shifts.find((option) => option.value === formData.EMPSHIFT) || null}
                            // onChange={(selectedOption) =>
                            //   handleSelectChange(selectedOption, 'EMPSHIFT')
                            // }
                            onChange={(selectedOption) => {
                              setFormData((prevData) => ({
                                ...prevData,
                                EMPSHIFT: selectedOption ? selectedOption.value : "",
                              }));
                              if (departmentRef.current) {
                                departmentRef.current.focus();
                              }
                            }}
                            // onKeyDown={(e) => handleEnterKey(e, departmentRef)}
                            ref={shiftRef}
                            placeholder={`${isRestricted ? "Select" : "निवडा"}`}
                          />
                        </div>
                      </div>

                      {!isRestricted && (
                        <div className="col-lg-3 col-md-6 col-12">
                          <div className="mb-3">
                            <label className="form-label">{isRestricted ? "Department" : "विभाग"}</label>

                            <Select
                              classNamePrefix="react-select"
                              name="EMPDEPARTMENT"
                              options={departments}
                              value={departments.find((option) => option.value === formData.EMPDEPARTMENT) || null}
                              onChange={(selectedOption) => {
                                setFormData((prevData) => ({
                                  ...prevData,
                                  EMPDEPARTMENT: selectedOption ? selectedOption.value : "",
                                }));
                                if (designationRef.current) {
                                  designationRef.current.focus();
                                }
                              }}
                              ref={departmentRef}
                              placeholder={`${isRestricted ? "Select" : "निवडा"}`}
                            />
                          </div>
                        </div>
                      )}

                      <div className="col-lg-3 col-md-6 col-12">
                        <div className="mb-3">
                          <label className="form-label">{isRestricted ? "Aadhaar No" : "आधार क्रमांक"}</label>
                          <input
                            type="text"
                            name="AADHAAR"
                            maxLength="12"
                            className="form-control"
                            placeholder={isRestricted ? "Enter Aadhaar No" : "आधार क्रमांक प्रविष्ट करा"}
                            value={formData.AADHAAR}
                            onChange={(e) => {
                              const numericValue = e.target.value.replace(/\D/g, ""); // Only digits
                              setFormData((prevData) => ({
                                ...prevData,
                                AADHAAR: numericValue,
                              }));
                            }}
                            onKeyDown={(e) => {
                              // Optionally prevent typing non-numeric keys
                              if (!/[0-9]|Backspace|ArrowLeft|ArrowRight|Tab/.test(e.key) && e.key.length === 1) {
                                e.preventDefault();
                              }
                            }}
                          />
                        </div>
                      </div>


                    </div>
                  </div>

                  {/* Address */}
                  <div
                    className="accordion-card-one accordion mt-2"
                    id="accordionExample2"
                  >
                    <div className="accordion-item mbgcolor">
                      <div className="accordion-header" id="headingTwo">
                        <div
                          className=""
                          data-bs-toggle="collapse"
                          data-bs-target="#collapseTwo"
                          aria-controls="collapseTwo"
                        >
                          <div className="text-editor add-list">
                            <div className="card-title-head">
                              <h6>
                                <span>
                                  <Info className="feather-edit" />
                                </span>
                                {isRestricted ? "Address" : "पत्ता"}

                              </h6>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div
                        id="collapseTwo"
                        className="accordion-collapse collapse show"
                        aria-labelledby="headingTwo"
                        data-bs-parent="#accordionExample2"
                      >


                        <div className="addservice-info">
                          <div className="row">

                            <div className="col-lg-4 col-sm-6 col-6">
                              <div className="mb-3 add-product">
                                <label className="form-label required">{isRestricted ? "Flat/Bulding" : "फ्लॅट,इमारत"}</label>
                                <input type="text"
                                  className="form-control"
                                  name="EMPADDRESS"
                                  value={formData.EMPADDRESS}
                                  onChange={handleChange}
                                  onKeyDown={(e) => handleEnterKey(e, areaRef)}
                                  ref={addressRef}
                                  required
                                />
                              </div>
                            </div>
                            <div className="col-lg-4 col-sm-6 col-6">
                              <div className="mb-3 add-product">
                                <label className="form-label required">{isRestricted ? "Village" : "गाव"} </label>
                                <input type="text"
                                  className="form-control"
                                  name="EMPAREA"
                                  value={formData.EMPAREA}
                                  onKeyDown={(e) => handleEnterKey(e, landmarkRef)}
                                  ref={areaRef}
                                  onChange={handleChange}
                                  required />
                              </div>
                            </div>

                            <div className="col-lg-4 col-sm-6 col-6">
                              <div className="mb-3 add-product">
                                <label className="form-label">{isRestricted ? "Land-Mark" : "प्रमुख ठिकाण"}</label>
                                <input type="text" className="form-control"
                                  name="EMPLANDMARK"
                                  value={formData.EMPLANDMARK}
                                  onChange={handleChange}
                                  onKeyDown={(e) => handleEnterKey(e, cityRef)}
                                  ref={landmarkRef}
                                />
                              </div>
                            </div>
                            {/* </div>
                        </div>
                        <div className="add-product-new">
                          <div className="row"> */}

                            <div className="col-lg-4 col-sm-6 col-6">
                              <div className="mb-3 add-product">
                                <label className="form-label required">{isRestricted ? "Pincode" : "पिनकोड"} </label>
                                <input type="text"
                                  className="form-control"
                                  name="EMPPINCODE"
                                  maxLength={6}
                                  value={formData.EMPPINCODE}
                                  onChange={handleChange}
                                  pattern="^\d{6}$"
                                  title="Countain only six digit number ."
                                  onKeyDown={(e) => handleEnterKey(e, addressRef)}
                                  ref={pincodeRef}
                                  required
                                />
                              </div>
                            </div>
                            <div className="col-lg-4 col-sm-6 col-6">
                              <div className="mb-3 add-product">
                                <label className="form-label"> {isRestricted ? "City" : "शहर"}</label>
                                <Select
                                  classNamePrefix="react-select"
                                  name="EMPCITY"
                                  options={sellingtype}
                                  value={sellingtype.find((option) => option.value === formData.EMPCITY) || null}
                                  // onChange={(selectedOption) => handleSelectChange(selectedOption, 'EMPCITY')}
                                  onChange={(selectedOption) => {
                                    setFormData((prevData) => ({
                                      ...prevData,
                                      EMPCITY: selectedOption ? selectedOption.value : "",
                                    }));
                                    if (stateRef.current) {
                                      stateRef.current.focus();
                                    }
                                  }}
                                  // onKeyDown={(e) => handleEnterKey(e, stateRef)}
                                  ref={cityRef}
                                  placeholder={`${isRestricted ? "Select" : "निवडा"}`}
                                />

                              </div>
                            </div>
                            <div className="col-lg-4 col-sm-6 col-6">
                              <div className="mb-3 add-product">
                                <label className="form-label">{isRestricted ? "State" : "राज्य"}</label>
                                <Select
                                  classNamePrefix="react-select"
                                  name="EMPSTATE"
                                  options={statetype}
                                  value={statetype.find((option) => option.value === formData.EMPSTATE) || null}
                                  // onChange={(selectedOption) => handleSelectChange(selectedOption, 'EMPSTATE')}
                                  onChange={(selectedOption) => {
                                    setFormData((prevData) => ({
                                      ...prevData,
                                      EMPSTATE: selectedOption ? selectedOption.value : "",
                                    }));
                                    if (submitRef.current) {
                                      submitRef.current.focus();
                                    }
                                  }}
                                  // onKeyDown={(e) => handleEnterKey(e, stateRef)}
                                  ref={stateRef}
                                  placeholder={`${isRestricted ? "Select" : "निवडा"}`}
                                />
                              </div>
                            </div>
                          </div>
                        </div>

                      </div>
                    </div>
                  </div>

                </div>
              </div>
            </div>

            <div className="col-lg-12 mt -2">
              <div className="text-end">
                <div className="row justify-content-end">
                  {/* Save Button - First on mobile, second on desktop */}
                  <div className="col-12 col-md-1 mb-2 order-1 order-md-2">
                    <button
                      type="submit"
                      className="btn btn-submit w-100"
                      ref={submitRef}
                    >
                      {isRestricted ? "Save" : "सेव्ह"}
                    </button>
                  </div>

                  {/* Back Button - Second on mobile, first on desktop */}
                  <div className="col-12 col-md-1 mb-2 order-2 order-md-1">
                    <button
                      type="button"
                      className="btn btn-cancel w-100"
                      onClick={showExitAlert}
                    >
                      {isRestricted ? "Back" : "मागे"}
                    </button>
                  </div>
                </div>
              </div>
            </div>

            {/* /product list */}
            {/* <div className="text-end mb-3">
              <button
                type="button"
                className="btn btn-cancel me-2"
                data-bs-dismiss="modal"
                onClick={showExitAlert}
              >
                बाहेर पडा
              </button>
              <button type="submit" className="btn btn-submit"
                ref={submitRef}>
                जतन करा
              </button>
            </div> */}
          </form>
        </div>
      </div>
    </div>
  );
};

export default AddEmployee;
