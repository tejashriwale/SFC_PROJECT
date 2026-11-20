import axios from "axios";

// userData.js
const userData = JSON.parse(localStorage.getItem("userData")) || {
    isAuthenticated: false,
    userdetail: null,
};

// Attach the JWT (if any) to every API call
const setAuthToken = (token) => {
    if (token) {
        axios.defaults.headers.common.Authorization = `Bearer ${token}`;
    } else {
        delete axios.defaults.headers.common.Authorization;
    }
};
setAuthToken(userData.token);

// If the server rejects our token, drop the session and go back to the sign-in page
axios.interceptors.response.use(
    (res) => res,
    (err) => {
        if (err?.response?.status === 401 && userData.token) {
            clearUserData();
            window.location.href = "/signin";
        }
        return Promise.reject(err);
    }
);

const getUserData = () => userData;

const updateUserData = (newData) => {
    Object.assign(userData, newData);
    localStorage.setItem("userData", JSON.stringify(userData));
};

function clearUserData() {
    Object.assign(userData, { isAuthenticated: false, userdetail: null, token: null });
    setAuthToken(null);
    localStorage.removeItem("userData");
}

export { getUserData, updateUserData, clearUserData, setAuthToken };
