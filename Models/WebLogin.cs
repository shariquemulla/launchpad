using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;

namespace Boilerplate.Models {

	public class WebLogin : DbContext {
		// private variables

		private DbSet<User> tblUser { get; set; }
		private HttpContext context;

		private string _username;
		private string _password;
		private bool _access;

		public WebLogin(HttpContext myHttpContext) {
			_username = "";
			_password = "";
			_access = false;

			context = myHttpContext;

			// clear out the session variable
			context.Session.Clear();
		}

		public string username {
			set { _username = value == null ? "" : value; }
		}

		public string password {
			set { _password = value == null ? "" : value; }
		}

		public bool access {
			get { return _access; }
		}

		// public methods
		public bool unlock() {
			// assume no access
			_access = false;

			// trim username and password to 10 chars
			_username = truncate(_username, 10);
			_password = truncate(_password, 10);

			User user = tblUser.Where(u => u.username == _username).FirstOrDefault();
			if(user == null) {
				return _access;
			}

			string hashedPassword = getHashed(_password, user.salt);

			if(hashedPassword == user.password) {
				_access = true;
				context.Session.SetString("auth", "true");
				context.Session.SetString("user", _username);
			}
			return _access;
		}

		public void logout() {
			// clear out the session variable
			context.Session.Clear();
		}

		// ------------------------------------------------------- private methods
		private string getSalt() {
			// generate a 128-bit salt using a secure PRNG (pseudo-random number generator)
			byte[] salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create()) {
				rng.GetBytes(salt);
			}
			//Console.WriteLine(">>> Salt: " + Convert.ToBase64String(salt));

			return Convert.ToBase64String(salt);
		}

		private string getHashed(string myPassword, string mySalt) {
			// convert string to Byte[] for hashing
			byte[] salt = Convert.FromBase64String(mySalt);
	
			// hashing done using PBKDF2 algorithm
			// derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
			string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: myPassword,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 8));
			//Console.WriteLine(">>> Hashed: " + hashed);

			return hashed;
		}

		// cuts down [value] to [maxLength] characters
		private string truncate(string value, int maxLength) {
			return value.Length <= maxLength ? value : value.Substring(0, maxLength); 
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySql(Connection.CONNECTION_STRING, new MySqlServerVersion(new Version(8, 0, 11)));
        }

	}
}