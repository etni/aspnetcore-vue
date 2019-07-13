import axios from 'axios'
const IDENTITY = '/api/identity'

export default {
  login: (username, password) => axios.post(`${IDENTITY}/login`, { username, password }),
  logout: () => axios.get(`${IDENTITY}/logout`),
  whoami: () => axios.get(`${IDENTITY}/whoami`)
}
