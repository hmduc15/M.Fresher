import axios from "axios";

const request = axios.create({
     baseURL: process.env.VUE_APP_API_URL
})

request.interceptors.response.use(
     (response) => {
          return response;
     },
     (error) => {
          if (error.response.data.DataError) {
               var errCode = error.response.data.ErrorCode;
               var listError = error.response.data.DataError;
               console.log(errCode, listError);
               switch (errCode) {
                    case 400:
                         return Promise.reject({ message: listError, statusCode: errCode });
               }

          }
     }

)

export default request