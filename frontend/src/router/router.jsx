import { BrowserRouter, Routes, Route } from 'react-router-dom';

import LandingPage from '../pages/LandingPage';
import Termsofuse from '../pages/TermsOfUse/termsofuse';
import WeRemoveGoogleReview from '../pages/WeRemoveGoogleReview/WeRemoveGoogleReview';
import WeRemoveGoogleSearch from '../pages/WeRemoveGoogleSearch/WeRemoveGoogleSearch';
import Carrerpg1 from '../pages/Carrer/Carrerpg1';
import Privacy from '../pages/Privacy Policy/Privacy';

const Router = () => {
	return (
		<BrowserRouter>
			<Routes>
				<Route path="/" element={<LandingPage />} />

				<Route
					path="we-remove-google-search"
					element={<WeRemoveGoogleSearch />}
				/>

				<Route
					path="we-remove-google-review"
					element={<WeRemoveGoogleReview />}
				/>

				<Route path="terms-of-use" element={<Termsofuse />} />

				<Route path="privacy" element={<Privacy />} />

				<Route path="career" element={<Carrerpg1 />} />
			</Routes>
		</BrowserRouter>
	);
};
export default Router;
