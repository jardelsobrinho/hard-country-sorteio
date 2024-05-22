import axios from 'axios'

export const client = axios.create({
  baseURL: 'http://localhost:5260/',
  headers: {
    'Content-Type': 'application/json'
  }
})