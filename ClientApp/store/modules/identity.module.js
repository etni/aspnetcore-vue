import api, {AuthenticationError} from '../../api/identity'
import router from '../../router'

const state = {
  authenticating: false,
  profile: null,
  authenticationErrorCode: 0,
  authenticationErrorData: null,
  name: 'identity module'
}

const getters = {
  loggedIn: state => {
    return !!state.profile
  },
  profile: state => state.profile,
  authenticationErrorCode: state => state.authenticationErrorCode,
  authenticationErrorData: state => state.authenticationErrorData,
  authenticating: state => state.authenticating,
  testname: state => state.name
}

const actions = {
  async login ({ commit }, {username, password}) {
    commit('loginRequest')

    try {
      const response = await api.login(username, password)
      commit('loginSuccess', response.data)
      // router.push(router.history.current.query.redirect || '/')
      console.log('Logged IN... should go now to home')
      return true
    } catch (e) {
      //
      commit('loginError', {
        errorCode: e.errorCode,
        errorData: e.errorData
      })
      // if (e instanceof AuthenticationError) {
      //   console.log("is instance")
      //   commit('loginError', {errorCode: e.errorCode, errorData: e.errorData})
      // }
      return false
    }
  },

  async logout ({commit}) {
    await api.logout()
    commit('logoutSuccess')
    router.push('/login')
  }

}

const mutations = {
  loginRequest (state) {
    state.profile = null
    state.authenticating = true
    state.authenticationErrorData = null
    state.authenticationErrorCode = 0
  },

  loginSuccess (state, profile) {
    state.profile = profile
    state.authenticating = false
  },

  loginError (state, { errorCode, errorData }) {
    state.authenticating = false
    state.authenticationErrorCode = errorCode
    state.authenticationErrorData = errorData
  },

  logoutSuccess (state) {
    state.profile = null
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
