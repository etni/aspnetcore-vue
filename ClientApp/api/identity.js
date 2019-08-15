import axios from 'axios'
const IDENTITY = '/api/identity'

// from https://medium.com/@zitko/structuring-a-vue-project-authentication-87032e5bfe16

class AuthenticationError extends Error {
  constructor (errorCode, message) {
    super('Cannot Authenticate')
    //this.name = this.contructor.name
    this.errorData = message
    this.errorCode = errorCode
  }
}

const IdentityApi = {
  login: async function (username, password) {
    console.log('start login', {username, password})
    try {
      const response = await axios.post(`${IDENTITY}/login`, { username, password })
      console.log('on login (response.data)', response)
      return response
    } catch (error) {
      console.log('login error', error.response.data)
      throw new AuthenticationError(error.response.status, error.response.data)
    }
  },
  logout: () => axios.get(`${IDENTITY}/logout`),
  whoami: () => axios.get(`${IDENTITY}/whoami`)
}

export default IdentityApi
export { IdentityApi, AuthenticationError }
