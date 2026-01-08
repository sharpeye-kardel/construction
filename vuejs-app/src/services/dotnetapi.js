import axios from 'axios';

const dotnetApi = axios.create({
  baseURL: import.meta.env.VITE_APP_BASE_URL_DOTNET
});

export const createApiData = data => {
  return dotnetApi.post('/constructions', data);
};

export const updateApiData = (id, data) => {
  return dotnetApi.put(`/constructions/${id}`, data);
};

export const deleteApiData = (id) => {
  return dotnetApi.delete(`/constructions/${id}`);
};

export const loginApiData = (data) => {
  console.log("base url is ", import.meta.env.VITE_APP_BASE_URL_DOTNET);
  return dotnetApi.post(`/users/login`, data);
};

