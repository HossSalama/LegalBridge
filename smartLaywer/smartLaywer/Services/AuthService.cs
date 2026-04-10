using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartLaywer.Services
{
    public interface IAuthService
    {
        Task<bool> IsAuthenticatedAsync();
        Task<LoginResult> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<string?> GetUserNameAsync();
        Task<string?> GetUserRoleAsync();
    }

    public record LoginResult(bool Success, string? Message = null);


    // ============================================================
    // AuthService.cs  –  Implementation (uses localStorage via JS)
    // ============================================================
    // Add this file to your Services/ folder.
    // Register in MauiProgram.cs:
    //   builder.Services.AddScoped<IAuthService, AuthService>();
    // ============================================================



    public class AuthService : IAuthService
    {
        private readonly IJSRuntime _js;

        public AuthService(IJSRuntime js)
        {
            _js = js;
        }

        // ── Check if user is logged in ──────────────────────────
        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var value = await _js.InvokeAsync<string?>(
                    "localStorage.getItem",
                    new object[] { "isLoggedIn" }
                );
                return value == "true";
            }
            catch (Exception)
            {
                // JS not ready yet in MAUI startup
                return false;
            }
        }

        // ── Login ───────────────────────────────────────────────
        // TODO: Replace mock logic with real API call
        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            // ── Mock validation ──────────────────────────────────
            // In production replace this with:
            //   var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { email, password });
            //   if (!response.IsSuccessStatusCode) return new LoginResult(false, "بيانات غير صحيحة");
            //   var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
            //   await _js.InvokeVoidAsync("localStorage.setItem", "token", token.AccessToken);
            // ─────────────────────────────────────────────────────

            await Task.Delay(600); // simulate network call

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return new LoginResult(false, "يرجى إدخال البريد الإلكتروني وكلمة المرور");

            // Mock: any non-empty credentials succeed
            var userName = email.Split('@')[0]; // use email prefix as name
            await _js.InvokeVoidAsync("localStorage.setItem", "isLoggedIn", "true");
            await _js.InvokeVoidAsync("localStorage.setItem", "userRole", "Admin");
            await _js.InvokeVoidAsync("localStorage.setItem", "userName", userName);

            return new LoginResult(true);
        }

        // ── Logout ──────────────────────────────────────────────
        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "isLoggedIn");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userRole");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userName");
        }

        // ── Helpers ─────────────────────────────────────────────
        public async Task<string?> GetUserNameAsync()
        {
            try { return await _js.InvokeAsync<string?>("localStorage.getItem", "userName"); }
            catch { return null; }
        }

        public async Task<string?> GetUserRoleAsync()
        {
            try { return await _js.InvokeAsync<string?>("localStorage.getItem", "userRole"); }
            catch { return null; }
        }
    }
}
// ============================================================
// IAuthService.cs  –  Interface
// ============================================================



