import configData from "../config.json"

export const GOOGLE_API_KEY = configData.GOOGLE_API_KEY != '' ? configData.GOOGLE_API_KEY : process.env.GOOGLE_API_KEY;